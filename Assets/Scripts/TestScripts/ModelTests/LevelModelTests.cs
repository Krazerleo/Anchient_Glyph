using System.Linq;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Traits;
using AncientGlyph.GameScripts.GameWorldModel;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    [TestFixture]
    public class LevelModelTests
    {
        [Test]
        public void MoveEntity()
        {
            var levelModel = new LevelModel();
            var oldPositionOfCreature = new Vector3Int(5, 0, 5);
            var offset = new Vector3Int(1, 0, 0);
            var newPositionOfCreature = oldPositionOfCreature + offset;
            var creature = new CreatureModel(ScriptableObject.CreateInstance<CreatureTraits>(),
                "test", oldPositionOfCreature);

            levelModel[oldPositionOfCreature].AddEntityToCell(creature);

            creature.TryMoveToNextCell(offset, levelModel);

            Assert.AreEqual(0, levelModel[oldPositionOfCreature]
                .GetEntitiesFromCell().Count());

            Assert.AreEqual(1, levelModel[newPositionOfCreature]
                .GetEntitiesFromCell().Count());

            Assert.NotNull(levelModel[newPositionOfCreature]
                .GetEntitiesFromCell()
                .First() as CreatureModel);

            Assert.AreEqual(newPositionOfCreature,
                levelModel[newPositionOfCreature]
                    .GetEntitiesFromCell()
                    .First().Position);
        }

        [Test]
        public void GetAllEntities()
        {
            LevelModel levelModel = new();
            const int totalCreatures = 10;

            for (var i = 0; i < totalCreatures; i++)
            {
                Vector3Int position = new(i * 2, 0, i * 3);

                CreatureModel creature = new(ScriptableObject.CreateInstance<CreatureTraits>(), "testing", position);

                levelModel[position].AddEntityToCell(creature);
            }

            Assert.AreEqual(totalCreatures, levelModel.GetAllCurrentEntities().Count());
        }
    }
}