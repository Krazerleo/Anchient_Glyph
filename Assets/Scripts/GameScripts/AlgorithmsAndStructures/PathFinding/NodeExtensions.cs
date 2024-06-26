using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    public static class NodeExtensions
    {
        private static readonly IReadOnlyCollection<(Vector3Int offset, float cost)> NeighboursThreeAxisTemplate =
            new List<(Vector3Int offset, float cost)>
            {
                (new Vector3Int(1,  0,  0), 1),
                (new Vector3Int(-1, 0,  0), 1),
                (new Vector3Int(0,  0,  1), 1),
                (new Vector3Int(0,  0,  -1), 1),
                (new Vector3Int(0,  1,  0), 2),
                (new Vector3Int(0,  -1, 0), 2),
            };

        private static readonly IReadOnlyCollection<(Vector3Int offset, float cost)> NeighboursTwoAxisTemplate =
            new List<(Vector3Int offset, float cost)>
            {
                (new Vector3Int(1,  0, 0), 1),
                (new Vector3Int(-1, 0, 0), 1),
                (new Vector3Int(0,  0, 1), 1),
                (new Vector3Int(0,  0, -1), 1),
            };

        public static void FillForPoint(this PathNode[] buffer, PathNode parent, Vector3Int target, int freeAxis)
        {
            IReadOnlyCollection<(Vector3Int offset, float cost)> template = FillTemplate(freeAxis);

            int bufferIndex = 0;
            foreach ((Vector3Int offset, float cost) in template)
            {
                Vector3Int nodePosition = offset + parent.Position;
                float traverseDistance = parent.TraverseDistance + cost;
                buffer[bufferIndex++] = new PathNode(nodePosition, target, traverseDistance);
            }
        }

        public static void FillForLine(this PathNode[] buffer, PathNode parent, Vector3Int target, int freeAxis)
        {
            IReadOnlyCollection<(Vector3Int offset, float cost)> template = FillTemplate(freeAxis);

            int bufferIndex = 0;
            foreach ((Vector3Int offset, float cost) in template)
            {
                Vector3Int nodePosition = offset + parent.Position;
                float traverseDistance = parent.TraverseDistance + cost;

                Vector3Int xProjection = new(nodePosition.x, nodePosition.y, target.z);
                Vector3Int zProjection = new(target.x, nodePosition.y, nodePosition.z);

                buffer[bufferIndex++] = new PathNode(nodePosition, target, traverseDistance);
            }
        }

        private static IReadOnlyCollection<(Vector3Int offset, float cost)> FillTemplate(int freeAxis)
        {
            IReadOnlyCollection<(Vector3Int offset, float cost)> template = freeAxis switch
            {
                2 => NeighboursTwoAxisTemplate,
                3 => NeighboursThreeAxisTemplate,
                _ => throw new ArgumentOutOfRangeException(nameof(freeAxis)),
            };

            return template;
        }
    }
}