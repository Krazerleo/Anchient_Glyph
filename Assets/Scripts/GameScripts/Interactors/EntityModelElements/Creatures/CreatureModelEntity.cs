using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interaction;

using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    public class CreatureEntityModel : IInteractable, IEntityModel
    {
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