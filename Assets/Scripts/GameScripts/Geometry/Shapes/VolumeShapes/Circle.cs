using System.Collections.Generic;

using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes
{
    public class Circle : IShape3D
    {
        #region Public Methods

        public IEnumerable<Vector3Int> GetDefinedGeometry()
        {
            throw new System.NotImplementedException();
        }

        #endregion Public Methods
    }
}