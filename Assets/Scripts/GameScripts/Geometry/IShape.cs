using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry
{
    /// <summary>
    /// 3D ceil geometry representation
    /// </summary>
    public interface IShape
    {
        public IEnumerable<Vector3Int> GetDefinedGeometry();
    }
}