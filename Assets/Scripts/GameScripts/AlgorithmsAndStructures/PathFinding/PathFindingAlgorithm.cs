﻿using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    /// <summary>
    /// Typical A* Algorithm
    /// </summary>
    public class PathFindingAlgorithm
    {
        private const int MaxNeighbours = 6;
        private readonly int _maxSteps;
        private readonly PathNode[] _neighbours = new PathNode[MaxNeighbours];
        private readonly List<Vector3Int> _output;
        
        private readonly IPriorityQueue<Vector3Int, PathNode> _frontier;
        private readonly HashSet<Vector3Int> _ignoredPositions;
        private readonly Dictionary<Vector3Int, Vector3Int> _links;
        private readonly LevelModel _levelModel;

        public PathFindingAlgorithm(LevelModel levelModel, int maxSteps = int.MaxValue, int initialCapacity = 0)
        {
            if (maxSteps <= 0) 
                throw new ArgumentOutOfRangeException(nameof(maxSteps));
            
            if (initialCapacity < 0) 
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            
            _levelModel = levelModel;
            _maxSteps = maxSteps;
            
            var comparer = Comparer<PathNode>.Create((a, b) => b.EstimatedTotalCost.CompareTo(a.EstimatedTotalCost));
            _frontier = new BinaryHeap<Vector3Int, PathNode>(comparer, node => node.Position, initialCapacity);
            
            _ignoredPositions = new HashSet<Vector3Int>(initialCapacity);
            _output = new List<Vector3Int>(initialCapacity);
            _links = new Dictionary<Vector3Int, Vector3Int>(initialCapacity);
        }

        public IReadOnlyCollection<Vector3Int> Calculate(Vector3Int start, Vector3Int target)
        {
            if (!GenerateNodes(start, target))
                return Array.Empty<Vector3Int>();

            _output.Clear();
            _output.Add(target);

            while (_links.TryGetValue(target, out target)) 
                _output.Add(target);

            return _output.AsReadOnly();
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
                var current = _frontier.Dequeue();
                _ignoredPositions.Add(current.Position);

                if (current.Position.Equals(target))
                {
                    return true;
                }

                GenerateFrontierNodes(current, target);
            }

            return false;
        }

        private void GenerateFrontierNodes(PathNode parent, Vector3Int target)
        {
            _neighbours.Fill(parent, target);
            
            foreach (var newNode in _neighbours)
            {
                if (_ignoredPositions.Contains(newNode.Position)) continue;

                var newNodeCell = _levelModel.At(newNode.Position);
                var targetNodeCell = _levelModel.At(target);
                var offset = newNode.Position - target;

                if (newNodeCell.CheckIsReachable(targetNodeCell, offset) == false)
                {
                    continue;
                }

                if (!_frontier.TryGet(newNode.Position, out PathNode existingNode))
                {
                    _frontier.Enqueue(newNode);
                    _links[newNode.Position] = parent.Position;
                }

                else if (newNode.TraverseDistance < existingNode.TraverseDistance)
                {
                    _frontier.Modify(newNode);
                    _links[newNode.Position] = parent.Position;
                }
            }
        }
    }
}