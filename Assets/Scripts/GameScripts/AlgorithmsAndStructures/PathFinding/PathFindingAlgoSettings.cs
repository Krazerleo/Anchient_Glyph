using System;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    public readonly struct PathFindingAlgoSettings
    {
        public readonly int DoF;
        public readonly int MaxSteps;
        public int MaxNeighbours => DoF * 2;
        
        /// <summary>
        /// Action to fill Neighbour Nodes of Parent Node with heuristic distance values
        /// relative to target position, considering degree of freedom.
        /// </summary>
        public readonly Action<PathNode[], PathNode, Vector3Int, int> HeuristicDistanceFunction;

        public PathFindingAlgoSettings(int doF, int maxSteps,
            Action<PathNode[], PathNode, Vector3Int, int> heuristicDistanceFunction)
        {
            DoF = doF;
            MaxSteps = maxSteps;
            HeuristicDistanceFunction = heuristicDistanceFunction;
        }
    }
}