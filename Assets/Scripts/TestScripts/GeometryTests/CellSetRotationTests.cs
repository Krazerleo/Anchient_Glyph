using System.Collections.Generic;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.GeometryTests
{
    [TestFixture]
    public class CellSetRotationTests
    {
        [Test]
        public void CellSetRotation_Square2x2()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(-1, -1), new Vector2Int(0, -1),
                new Vector2Int(-1, 0), new Vector2Int(0,  0),
            };

            CellSet cellSet = new(cellList);

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(-1, -1), new Vector2Int(0, -1),
                    new Vector2Int(-1, 0), new Vector2Int(0,  0),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(0);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(-1, -1), new Vector2Int(0, -1),
                    new Vector2Int(-1, 0), new Vector2Int(0,  0),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(4);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }
        }

        [Test]
        public void CellSetRotation_NotFullSquare2x2()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(-1, -1), new Vector2Int(0, -1),
                new Vector2Int(-1, 0),
            };

            CellSet cellSet = new(cellList);

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(-1, -1),
                    new Vector2Int(-1, 0), new Vector2Int(0, 0),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(1);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }


            {
                List<Vector2Int> expectedCellPositions2Rotations = new()
                {
                    new Vector2Int(0,  -1),
                    new Vector2Int(-1, 0), new Vector2Int(0, 0),
                };

                CellSet expectedCellSetTwoRotations = new(expectedCellPositions2Rotations);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(2);
                Assert.IsTrue(expectedCellSetTwoRotations.Equals(rotatedCellPositions));
            }
        }

        [Test]
        public void CellSetRotation_Square2x3()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0, 0), new Vector2Int(1, 0),
                new Vector2Int(0, 1), new Vector2Int(1, 1),
                new Vector2Int(0, 2), new Vector2Int(1, 2),
            };

            CellSet cellSet = new(cellList);

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
                    new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(1);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }
            
            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(0, 0), new Vector2Int(1, 0),
                    new Vector2Int(0, 1), new Vector2Int(1, 1),
                    new Vector2Int(0, 2), new Vector2Int(1, 2),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(2);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
                    new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(3);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }
        }

        [Test]
        public void CellSetRotation_NotFullSquare2x3()
        {
            List<Vector2Int> cellList = new()
            {
                new Vector2Int(0, 0), new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
            };

            CellSet cellSet = new(cellList);

            {
                List<Vector2Int> expectedCellPositions = new()
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
                };

                CellSet expectedCellSet = new(expectedCellPositions);
                CellSet rotatedCellPositions = cellSet.GetRotatedGeometry(1);
                Assert.IsTrue(expectedCellSet.Equals(rotatedCellPositions));
            }
        }
    }
}