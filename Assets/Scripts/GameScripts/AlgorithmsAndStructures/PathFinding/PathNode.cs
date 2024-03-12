using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    public readonly struct PathNode
    {
        public PathNode(Vector3Int position, Vector3Int target, float traverseDistance)
        {
            Position = position;
            TraverseDistance = traverseDistance;
            var heuristicDistance = (position - target).magnitude;
            EstimatedTotalCost = TraverseDistance + heuristicDistance;
        }
        
        public Vector3Int Position { get; }
        public float TraverseDistance { get; }
        public float EstimatedTotalCost { get; }
    }
}