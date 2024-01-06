using System.Collections.Generic;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes.Interfaces
{
    /// <summary>
    /// 2D ceil geometry representation
    /// </summary>
    public interface IShape2D
    {
        public IEnumerable<Vector2Int> GetDefinedGeometry();
    }
}