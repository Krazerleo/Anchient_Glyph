using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding
{
    public interface IPath
    {
        IReadOnlyCollection<Vector3Int> Calculate(Vector3Int start, Vector3Int target, 
            IReadOnlyCollection<Vector3Int> obstacles);
    }
}