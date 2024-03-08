using System.Linq;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Traits;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    public class LevelModelTests
    {
        [Test]
        public void MoveEntity()
        {
            var levelModel = new LevelModel();
            var creatureOldPosition = new Vector3Int(5, 0, 5);
            var offset = new Vector3Int(1, 0, 0);
            var creatureNewPosition = creatureOldPosition + offset;
            var creature = new CreatureModel(ScriptableObject.CreateInstance<CreatureTraits>(), 
                "test", creatureOldPosition);
            
            levelModel.At(creatureOldPosition).AddEntityToCell(creature);
            
            levelModel.TryMoveEntity(creature, offset);
            
            Assert.AreEqual(0, levelModel.At(creatureOldPosition)
                                                       .GetEntitiesFromCell().Count());

            Assert.AreEqual(1, levelModel.At(creatureNewPosition)
                                                       .GetEntitiesFromCell().Count());

            Assert.NotNull((levelModel.At(creatureNewPosition)
                .GetEntitiesFromCell()
                .First() as CreatureModel)!.Traits);
            
            Assert.AreEqual(levelModel.At(creatureNewPosition)
                .GetEntitiesFromCell()
                .First().Position, creatureNewPosition);
        }

        [Test]
        public void GetAllEntities()
        {
            var levelModel = new LevelModel();
            const int totalCreatures = 10;
            
            for (var i = 0; i < totalCreatures; i++)
            {
                var position = new Vector3Int(i * 2, 0, i * 3);

                var creature = new CreatureModel(ScriptableObject.CreateInstance<CreatureTraits>(),
                    "testing", position);
                
                levelModel.At(position).AddEntityToCell(creature);
            }
            
            Assert.AreEqual(totalCreatures, levelModel.GetAllCurrentEntities().Count());
        }
    }
}