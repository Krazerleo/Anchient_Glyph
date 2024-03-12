using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    internal static class NodeExtensions
    {
        private static readonly (Vector3Int offset, float cost)[] NeighboursTemplate =
        {
            (new Vector3Int(1, 0, 0), 1),
            (new Vector3Int(0, 1, 0), 1),
            (new Vector3Int(-1, 0, 0), 1),
            (new Vector3Int(0, -1, 0), 1),
            (new Vector3Int(0, 0, 1), 1),
            (new Vector3Int(0, 0, -1), 1),
        };

        public static void Fill(this PathNode[] buffer, PathNode parent, Vector3Int target)
        {
            int i = 0;

            foreach (var (offset, cost) in NeighboursTemplate)
            {
                var nodePosition = offset + parent.Position;
                float traverseDistance = parent.TraverseDistance + cost;
                buffer[i++] = new PathNode(nodePosition, target, traverseDistance);
            }
        }
    }
}