using System;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    internal static class NodeExtensions
    {
        private static readonly (Vector3Int offset, float cost)[] NeighboursThreeAxisTemplate =
        {
            (new Vector3Int(1, 0, 0), 1),
            (new Vector3Int(0, 1, 0), 1),
            (new Vector3Int(-1, 0, 0), 1),
            (new Vector3Int(0, -1, 0), 1),
            (new Vector3Int(0, 0, 1), 1),
            (new Vector3Int(0, 0, -1), 1),
        };
        
        private static readonly (Vector3Int offset, float cost)[] NeighboursTwoAxisTemplate =
        {
            (new Vector3Int(1, 0, 0), 1),
            (new Vector3Int(0, 1, 0), 1),
            (new Vector3Int(-1, 0, 0), 1),
            (new Vector3Int(0, -1, 0), 1),
        };

        public static void Fill(this PathNode[] buffer, PathNode parent, Vector3Int target, int freeAxis)
        {
            int i = 0;
            (Vector3Int offset, float cost)[] template;
            
            switch (freeAxis)
            {
                case 2:
                    template = NeighboursTwoAxisTemplate;
                    break;
                case 3:
                    template = NeighboursThreeAxisTemplate;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(freeAxis));
            }
            
            foreach (var (offset, cost) in template)
            {
                var nodePosition = offset + parent.Position;
                float traverseDistance = parent.TraverseDistance + cost;
                buffer[i++] = new PathNode(nodePosition, target, traverseDistance);
            }
        }
    }
}