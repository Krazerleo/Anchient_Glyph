using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry
{
    public class Rectangle : Shape
    {
        public Vector3Int StartCorner { get; private set; }
        public Vector3Int EndCorner { get; private set; }

        public Rectangle(Vector3Int startCorner, Vector3Int endCorner)
        {
            StartCorner = startCorner;
            EndCorner = endCorner;
        }

        public IEnumerable<Vector3Int> GetDefinedGeometry()
        {
            var yeldingCeilPosition = StartCorner;

            for (int i = StartCorner.x; i <= EndCorner.x; i++)
            {
                for (int j = StartCorner.y; j <= EndCorner.y; j++)
                {
                    for (int k = StartCorner.z; k <= EndCorner.z; k++)
                    {
                        yeldingCeilPosition.Set(i, j, k);
                        yield return yeldingCeilPosition;
                    }
                }
            }
        }
    }
}