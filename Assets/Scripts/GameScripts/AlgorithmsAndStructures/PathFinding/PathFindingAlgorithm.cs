#define DO_BENCHMARK

using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    public class PathFindingAlgorithm
    {
        private readonly PathFindingAlgoSettings _settings;

        private readonly PathNode[] _neighbours;
        private readonly List<Vector3Int> _output;
        private readonly IPriorityQueue<Vector3Int, PathNode> _frontier;
        private readonly HashSet<Vector3Int> _ignoredPositions;
        private readonly Dictionary<Vector3Int, Vector3Int> _links;
        private readonly LevelModel _levelModel;

        public PathFindingAlgorithm(LevelModel levelModel, PathFindingAlgoSettings settings, int collectionsCapacity = 0)
        {
            if (settings.MaxSteps <= 0)
                throw new ArgumentOutOfRangeException(nameof(settings.MaxSteps));

            if (collectionsCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(collectionsCapacity));

            _settings = settings;
            
            _levelModel = levelModel;
            _neighbours = new PathNode[_settings.MaxNeighbours];
            
            Comparer<PathNode> comparer = Comparer<PathNode>
                .Create((a, b) => b.EstimatedTotalCost.CompareTo(a.EstimatedTotalCost));
            _frontier = new BinaryHeap<Vector3Int, PathNode>(comparer, node => node.Position, collectionsCapacity);
            _ignoredPositions = new HashSet<Vector3Int>(collectionsCapacity);
            _output = new List<Vector3Int>(collectionsCapacity);
            _links = new Dictionary<Vector3Int, Vector3Int>(collectionsCapacity);
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

            while (_frontier.Count > 0 && step++ <= _settings.MaxSteps)
            {
                PathNode current = _frontier.Dequeue();
                _ignoredPositions.Add(current.Position);

                if (current.Position.Equals(target))
                {
                    return true;
                }

                GenerateFrontierNodes(current, target);
            }

            Debug.LogError("Calculating failed");
            return false;
        }

        private void GenerateFrontierNodes(PathNode parent, Vector3Int target)
        {
            _settings.HeuristicDistanceFunction(_neighbours, parent, target, _settings.DoF);

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