using System.Collections.Generic;

using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes
{
    public class Point : IShape
    {
        #region Public Fields

        public readonly Vector3Int Position;

        #endregion Public Fields

        #region Public Constructors

        public Point(Vector3Int position)
        {
            Position = position;
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<Vector3Int> GetDefinedGeometry()
        {
            yield return Position;
        }

        #endregion Public Methods
    }
}