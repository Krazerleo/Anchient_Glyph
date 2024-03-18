using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    [TestFixture]
    public class CellRayCasterTests
    {
        [Test]
        public void CellRayCast_TEST_NEIGHBOUR_DISTANCE_WITH_WALL()
        {
            var startCell = new CellModel(new WallType[] { 0, WallType.Whole, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);
            
            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 1);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition));
        }
        
        [Test]
        public void CellRayCast_TEST_NEIGHBOUR_DISTANCE_WITHOUT_WALL()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);
            
            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 1);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition) == false);
        }
        
        [Test]
        public void CellRayCast_TEST_FAR_DISTANCE_WITHOUT_WALLS()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 3);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition) == false);
        }
        
        [Test]
        public void CellRayCast_TEST_FAR_DISTANCE_WITH_WALLS()
        {
            var startCell = new CellModel(new WallType[] { 0, WallType.Whole, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 3);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition));
        }
        
        [Test]
        public void CellRayCast_TEST_FAR_DISTANCE_WITH_WALLS_1()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, WallType.Whole, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 3);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition));
        }
        
        [Test]
        public void CellRayCast_TEST_ON_REVERSE_Z()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 3);

            var endCell = new CellModel(new WallType[] { 0, WallType.Whole, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 0);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition));
        }
        
        [Test]
        public void CellRayCast_TEST_ON_REVERSE_X()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(3, 1, 0);

            var endCell = new CellModel(new WallType[] { WallType.Whole, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(0, 1, 0);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };
            
            Assert.IsTrue(levelModel.IsRayCollided(startPosition, endPosition));
        }
    }
}