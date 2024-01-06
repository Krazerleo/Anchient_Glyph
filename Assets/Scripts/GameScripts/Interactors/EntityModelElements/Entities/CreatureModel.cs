using System.Collections.Generic;

using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.Interactors.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    public class CreatureModel : IEntityModel
    {
        private Vector3Int _creaturePosition;
        private readonly CreatureTraits _creatureTraits;
        private readonly string _entityId;

        public CreatureModel(CreatureTraits traits, Vector3Int position, string entityId)
        {
            _creatureTraits = traits;
            _creaturePosition = position;
            _entityId = entityId;
        }

        public Vector3Int Position => _creaturePosition;

        public bool IsFullSize => _creatureTraits.IsFullSize;

        public string Identifier => _entityId;

        public string Name => _creatureTraits.CreatureName;

        public void InteractWith(IEntityModel targetEntity)
        {
            //TODO
        }

        public void AcceptInteraction(HitInteraction hit)
        {
            //TODO
        }

        public void AcceptInteraction(FunctionalInteraction func)
        {
            //TODO
        }

        public void AcceptInteraction(ICollection<object> listItems)
        {
            //TODO
        }
    }
}