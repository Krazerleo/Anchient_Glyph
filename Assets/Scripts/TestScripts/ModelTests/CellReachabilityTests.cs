using System;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    [TestFixture]
    public class CellReachabilityTests
    {
        [Test]
        public void CheckReachability_TEST_1()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });

            var startPosition = new Vector3Int(1, 1, 1);
            var endPosition = new Vector3Int(1, 1, 0);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            Assert.IsTrue(levelModel.CheckIsReachable(startPosition, endPosition));
        }

        [Test]
        public void CheckReachability_TEST_2()
        {
            var startCell = new CellModel(new WallType[] { 0, WallType.Whole, 0, 0, 0, 0 });
            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });

            var startPosition = new Vector3Int(1, 1, 0);
            var endPosition = new Vector3Int(1, 1, 1);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            Assert.IsFalse(levelModel.CheckIsReachable(startPosition, endPosition));
        }

        [Test]
        public void CheckReachability_TEST_3()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endCell = new CellModel(new WallType[] { 0, 0, 0, WallType.Whole, 0, 0 });

            var startPosition = new Vector3Int(1, 1, 0);
            var endPosition = new Vector3Int(1, 1, 1);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            Assert.IsFalse(levelModel.CheckIsReachable(startPosition, endPosition));
        }
        
        [Test]
        public void CheckReachability_TEST_FIXED_SWITCH()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endCell = new CellModel(new WallType[] { 0, 0, 0, WallType.Whole, 0, 0 });

            var startPosition = new Vector3Int(0, 0, 1);
            var endPosition = new Vector3Int(0, 0, 0);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            Assert.IsTrue(levelModel.CheckIsReachable(startPosition, endPosition));
        }

        [Test]
        public void CheckReachability_TEST_UB()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endCell = new CellModel(new WallType[] { 0, 0, 0, WallType.Whole, 0, 0 });

            var startPosition = new Vector3Int(1, 1, 0);
            var endPosition = new Vector3Int(1, 1, 5);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                levelModel.CheckIsReachable(startPosition, endPosition));
        }

        [Test]
        public void CheckReachability_TEST_SAME()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });

            var startPosition = new Vector3Int(1, 1, 0);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
            };

            Assert.IsTrue(levelModel.CheckIsReachable(startPosition, startPosition));
        }
    }
}