using System.Collections.Generic;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes.VolumeShapes
{
    public class Line : IShape3D
    {
        #region Public Methods

        public IEnumerable<Vector3Int> GetDefinedGeometry()
        {
            throw new System.NotImplementedException();
        }

        #endregion Public Methods
    }
}