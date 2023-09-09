using System.Collections.Generic;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes.Interfaces
{
    /// <summary>
    /// 3D ceil geometry representation
    /// </summary>
    public interface IShape
    {
        #region Public Methods

        public IEnumerable<Vector3Int> GetDefinedGeometry();

        #endregion Public Methods
    }
}