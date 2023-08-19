using NUnit.Framework;

using AncientGlyph.GameScripts.Interactors.Creatures;

namespace AncientGlyph.Tests.ModelTests
{
    public class InteractionTests
    {
        [Test]
        public void HitCheck()
        {
            var firstEntity = new CreatureEntityModel();
            var secondEntity = new CreatureEntityModel();

            firstEntity.Hit(secondEntity);
        }
    }
}
