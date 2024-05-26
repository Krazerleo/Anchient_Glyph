using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    /// <summary>
    /// Typical A* Algorithm with some modifications
    /// https://habr.com/ru/articles/513158/
    /// </summary>
    public class PathFindingAlgorithm
    {
        private readonly int _freeAxis;
        private readonly int _maxSteps;
        private int MaxNeighbours => 2 * _freeAxis;
        private readonly PathNode[] _neighbours;
        private readonly List<Vector3Int> _output;

        private readonly IPriorityQueue<Vector3Int, PathNode> _frontier;
        private readonly HashSet<Vector3Int> _ignoredPositions;
        private readonly Dictionary<Vector3Int, Vector3Int> _links;
        private readonly LevelModel _levelModel;

        public PathFindingAlgorithm(LevelModel levelModel, int freeAxis, int maxSteps = int.MaxValue, int initialCapacity = 0)
        {
            if (maxSteps <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxSteps));

            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));

            _levelModel = levelModel;
            _maxSteps = maxSteps;
            _freeAxis = freeAxis;
            _neighbours = new PathNode[MaxNeighbours];
            
            Comparer<PathNode> comparer = Comparer<PathNode>
                .Create((a, b) => b.EstimatedTotalCost.CompareTo(a.EstimatedTotalCost));
            _frontier = new BinaryHeap<Vector3Int, PathNode>(comparer, node => node.Position, initialCapacity);
            _ignoredPositions = new HashSet<Vector3Int>(initialCapacity);
            _output = new List<Vector3Int>(initialCapacity);
            _links = new Dictionary<Vector3Int, Vector3Int>(initialCapacity);
        }

        public bool TryCalculate(Vector3Int start, Vector3Int target,
            out IReadOnlyList<Vector3Int> path)
        {
            path = Array.Empty<Vector3Int>();
            
            if (!GenerateNodes(start, target))
                return false;

            _output.Clear();
            _output.Add(target);

            while (_links.TryGetValue(target, out target))
                _output.Add(target);

            path = _output.AsReadOnly();
            return true;
        }

        private bool GenerateNodes(Vector3Int start, Vector3Int target)
        {
            _frontier.Clear();
            _ignoredPositions.Clear();
            _links.Clear();

            _frontier.Enqueue(new PathNode(start, target, 0));
            int step = 0;

            while (_frontier.Count > 0 && step++ <= _maxSteps)
            {
                PathNode current = _frontier.Dequeue();
                _ignoredPositions.Add(current.Position);

                if (current.Position.Equals(target))
                {
                    return true;
                }

                GenerateFrontierNodes(current, target);
            }

            Debug.Log("Calculating failed");
            return false;
        }

        private void GenerateFrontierNodes(PathNode parent, Vector3Int target)
        {
            _neighbours.Fill(parent, target, _freeAxis);

            foreach (PathNode newNode in _neighbours)
            {
                if (_ignoredPositions.Contains(newNode.Position))
                {
                    continue;
                }

                if (_levelModel.CheckInBounds(newNode.Position) == false)
                {
                    continue;
                }

                if (_levelModel.CheckIsReachable(parent.Position, newNode.Position) == false)
                {
                    continue;
                }

                if (_frontier.TryGet(newNode.Position, out PathNode existingNode) == false)
                {
                    _frontier.Enqueue(newNode);
                    _links[newNode.Position] = parent.Position;
                }
                else if (newNode.TraverseDistance <= existingNode.TraverseDistance)
                {
                    _frontier.Modify(newNode);
                    _links[newNode.Position] = parent.Position;
                }
            }
        }
    }
}