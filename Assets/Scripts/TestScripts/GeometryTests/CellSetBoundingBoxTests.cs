using System.Collections.Generic;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.GeometryTests
{
    [TestFixture]
    public class CellSetBoundingBoxTests
    {
        [Test]
        public void BoundingBoxTest_Square2x2()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0,  0),
                new Vector2Int(-1, -1),
                new Vector2Int(0,  -1),
                new Vector2Int(-1, 0),
            };

            RectInt bbox = new CellSet(cellList).GetBoundingBox();

            Assert.That(bbox, Is.EqualTo(new RectInt(-1, -1, 2, 2)));
        }

        [Test]
        public void BoundingBoxTest_Square3x3()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(0, 3),
                new Vector2Int(1, 1),
                new Vector2Int(1, 2),
                new Vector2Int(1, 3),
                new Vector2Int(2, 1),
                new Vector2Int(2, 2),
                new Vector2Int(2, 3),
            };

            RectInt bbox = new CellSet(cellList).GetBoundingBox();

            Assert.That(bbox, Is.EqualTo(new RectInt(0, 1, 3, 3)));
        }
        
        [Test]
        public void BoundingBoxTest_NotFullSquare2x2()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0,  0),
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 0),
            };

            RectInt bbox = new CellSet(cellList).GetBoundingBox();

            Assert.That(bbox, Is.EqualTo(new RectInt(-1, -1, 2, 2)));
        }
        
        [Test]
        public void BoundingBoxTest_NotFullSquare3x3()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(0, 3),
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
            };

            RectInt bbox = new CellSet(cellList).GetBoundingBox();

            Assert.That(bbox, Is.EqualTo(new RectInt(0, 1, 3, 3)));
        }
    }
}