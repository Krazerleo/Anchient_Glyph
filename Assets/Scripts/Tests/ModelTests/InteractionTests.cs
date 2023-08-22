using NUnit.Framework;

using AncientGlyph.GameScripts.Interactors.Creatures;

namespace AncientGlyph.Tests.ModelTests
{
    public class InteractionTests
    {
        [Test]
        public void HitCheck()
        {
            var creatureTraits = new CreatureTraits();
            var firstEntity = new CreatureEntityModel(creatureTraits, new UnityEngine.Vector3Int());
            var secondEntity = new CreatureEntityModel(creatureTraits, new UnityEngine.Vector3Int());

            firstEntity.Hit(secondEntity);
        }
    }
}
