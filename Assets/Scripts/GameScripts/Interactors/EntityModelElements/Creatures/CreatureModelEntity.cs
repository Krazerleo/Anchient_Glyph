using System.Collections.Generic;

using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    public class CreatureEntityModel : IInteractable, IEntityModel
    {
        private CreatureTraits _creatureTraits;
        private Vector3Int _creaturePosition;

        public CreatureEntityModel(CreatureTraits traits, Vector3Int position)
        {
            _creatureTraits = traits;
            _creaturePosition = position;
        }

        public bool IsFullSize => _creatureTraits.IsFullSize;

        public Vector3Int Position => _creaturePosition;

        public void GetEntityInfo()
        {
            //TODO
        }

        public void Hit(IEntityModel toEntity)
        {
            //TODO
            new HitInteraction(10).InteractTo(toEntity);
        }

        public void InteractWith(HitInteraction hit)
        {
            //TODO
        }

        public void InteractWith(FunctionalInteraction func)
        {
            //TODO
        }

        public void InteractWith(ICollection<object> listItems)
        {
            //TODO
        }
    }
}