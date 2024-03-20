using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions;
using AncientGlyph.GameScripts.GameWorldModel;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests.ActionConditions
{
    [TestFixture]
    public class OnLineConditionTests
    {
        [Test]
        public void OnLineConditionTest_NOT_SATISFY_WALL()
        {
            var startCell = new CellModel(new WallType[] { 0, WallType.Whole, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var creatureModel = new CreatureModel(null, "test", startPosition);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 1);

            var playerModel = new PlayerModel(endPosition);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            var onLineCondition = new OnLineCondition(0, 2);
            var conditionSatisfied = onLineCondition.CanExecute(creatureModel, playerModel,
                new GroundedBehaviour(levelModel), levelModel);

            Assert.IsFalse(conditionSatisfied);
        }
        
        [Test]
        public void OnLineConditionTest_NOT_SATISFY_TOO_BIG_DISTANCE()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var creatureModel = new CreatureModel(null, "test", startPosition);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 10);

            var playerModel = new PlayerModel(endPosition);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            var onLineCondition = new OnLineCondition(0, 2);
            var conditionSatisfied = onLineCondition.CanExecute(creatureModel, playerModel,
                new GroundedBehaviour(levelModel), levelModel);

            Assert.IsFalse(conditionSatisfied);
        }
        
        [Test]
        public void OnLineConditionTest_SATISFIED()
        {
            var startCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var startPosition = new Vector3Int(1, 1, 0);

            var creatureModel = new CreatureModel(null, "test", startPosition);

            var endCell = new CellModel(new WallType[] { 0, 0, 0, 0, 0, 0 });
            var endPosition = new Vector3Int(1, 1, 1);

            var playerModel = new PlayerModel(endPosition);

            var levelModel = new LevelModel
            {
                [startPosition.x, startPosition.y, startPosition.z] = startCell,
                [endPosition.x, endPosition.y, endPosition.z] = endCell
            };

            var onLineCondition = new OnLineCondition(0, 2);
            var conditionSatisfied = onLineCondition.CanExecute(creatureModel, playerModel,
                new GroundedBehaviour(levelModel), levelModel);

            Assert.IsTrue(conditionSatisfied);
        }
    }
}