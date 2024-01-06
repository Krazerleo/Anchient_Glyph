using System.Collections.Generic;

using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes
{
    public class CellSet : IShape2D
    {
        private IEnumerable<Vector2Int> _ceils;

        public CellSet(IEnumerable<Vector2Int> ceils)
        {
            _ceils = ceils;
        }

        public IEnumerable<Vector2Int> GetDefinedGeometry()
        {
            foreach (var ceil in _ceils)
            {
                yield return ceil;
            }
        }
    }
}