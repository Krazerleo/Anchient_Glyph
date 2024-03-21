using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.OtherTests
{
    [TestFixture]
    public class CellSetRotationTests
    {
        [Test]
        public void CellSetRotation_TEST_ROTATION_0()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };

            var cellSet = new CellSet(cellList);
            
            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };

            var rotatedCellPositions = cellSet.GetRotatedGeometry(0);

            Assert.IsTrue(rotatedCellPositions.SequenceEqual(expectedCellPositions));

            var fourRotationsOnCellSet = cellSet.GetRotatedGeometry(4);
            
            Assert.IsTrue(fourRotationsOnCellSet.SequenceEqual(expectedCellPositions));
        }
        
        [Test]
        public void CellSetRotation_TEST_ROTATION_90()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };

            var cellSet = new CellSet(cellList);

            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(0, -1),
                new Vector2Int(1, -1),
                new Vector2Int(2, -1),
                new Vector2Int(0, -2),
            };
            
            var rotatedCellSet = cellSet.GetRotatedGeometry(1);

            Assert.IsTrue(rotatedCellSet.SequenceEqual(expectedCellPositions));
        }
        
        [Test]
        public void CellSetRotation_TEST_ROTATION_180()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };

            var cellSet = new CellSet(cellList);

            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(-1, -1),
                new Vector2Int(-1, -2),
                new Vector2Int(-1, -3),
                new Vector2Int(-2, -1),
            };
            
            var rotatedCellSet = cellSet.GetRotatedGeometry(2);

            Assert.IsTrue(rotatedCellSet.SequenceEqual(expectedCellPositions));
        }
        
        [Test]
        public void CellSetRotation_TEST_ROTATION_270()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };

            var cellSet = new CellSet(cellList);

            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(-1, 0),
                new Vector2Int(-2, 0),
                new Vector2Int(-3, 0),
                new Vector2Int(-1, 1),
            };
            
            var rotatedCellSet = cellSet.GetRotatedGeometry(3);

            Assert.IsTrue(rotatedCellSet.SequenceEqual(expectedCellPositions));
        }
        
        [Test]
        public void CellSetRotation_TEST_ROTATION_90_Reversed()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(-1, 0),
                new Vector2Int(-2, 0),
                new Vector2Int(-3, 0),
                new Vector2Int(-1, 1),
            };

            var cellSet = new CellSet(cellList);

            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };
            
            var rotatedCellSet = cellSet.GetRotatedGeometry(1);
            
            Assert.IsTrue(rotatedCellSet.SequenceEqual(expectedCellPositions));
        }
        
        [Test]
        public void CellSetRotation_TEST_ROTATION_180_Reversed()
        {
            var cellList = new List<Vector2Int>()
            {
                new Vector2Int(-1, -1),
                new Vector2Int(-1, -2),
                new Vector2Int(-1, -3),
                new Vector2Int(-2, -1),
            };

            var cellSet = new CellSet(cellList);

            var expectedCellPositions = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 0),
            };
            
            var rotatedCellSet = cellSet.GetRotatedGeometry(2);
            
            Assert.IsTrue(rotatedCellSet.SequenceEqual(expectedCellPositions));
        }
    }
}