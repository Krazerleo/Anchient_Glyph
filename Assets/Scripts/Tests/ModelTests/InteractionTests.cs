using NUnit.Framework;

using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Traits;

using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    public class InteractionTests
    {
        [Test]
        public void HitCheck()
        {
            var creatureTraits = ScriptableObject.CreateInstance<CreatureTraits>();
            var firstEntity = new CreatureModel(creatureTraits, new Vector3Int(), "a", "");
            var secondEntity = new CreatureModel(creatureTraits, new Vector3Int(), "b", "");

            firstEntity.InteractWith(secondEntity);
        }
    }
}
