using System;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities.Traits;
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
            var firstEntity = new CreatureModel(creatureTraits, new Vector3Int(1,0,0), 
                Guid.NewGuid().ToString(), "");
            var secondEntity = new CreatureModel(creatureTraits, new Vector3Int(1,0,1),
                Guid.NewGuid().ToString(), "");

            firstEntity.InteractWith(secondEntity);
        }
    }
}
