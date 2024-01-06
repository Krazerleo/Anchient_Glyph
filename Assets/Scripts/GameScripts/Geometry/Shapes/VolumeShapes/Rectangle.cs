using System;
using System.Collections.Generic;

using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes
{
    public class Rectangle : IShape3D
    {
        public readonly Vector3Int StartCorner;
        public readonly Vector3Int EndCorner;

        public Rectangle(Vector3Int startCorner, Vector3Int endCorner)
        {
            StartCorner = startCorner;
            EndCorner = endCorner;
        }

        /// <summary>
        /// Get all cells in rectangle
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vector3Int> GetDefinedGeometry()
        {
            var yeldingCeilPosition = StartCorner;

            for (int i = Math.Min(StartCorner.x, EndCorner.x); i <= Math.Max(StartCorner.x,EndCorner.x); i++)
            {
                for (int j = Math.Min(StartCorner.y, EndCorner.y); j <= Math.Max(StartCorner.y, EndCorner.y); j++)
                {
                    for (int k = Math.Min(StartCorner.z, EndCorner.z); k <= Math.Max(StartCorner.z, EndCorner.z); k++)
                    {
                        yeldingCeilPosition.Set(i, j, k);
                        yield return yeldingCeilPosition;
                    }
                }
            }
        }
    }
}