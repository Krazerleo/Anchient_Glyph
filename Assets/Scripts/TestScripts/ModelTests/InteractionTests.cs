using System;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Traits;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    public class InteractionTests
    {
        [Test]
        public void HitCheck()
        {
            var creatureTraits = ScriptableObject.CreateInstance<CreatureTraits>();
            var firstEntity = new CreatureModel(creatureTraits, "a", new Vector3Int(1,0,0));
            var secondEntity = new CreatureModel(creatureTraits, "ab", new Vector3Int(1,0,1));

            firstEntity.InteractWith(secondEntity);
        }
    }
}
