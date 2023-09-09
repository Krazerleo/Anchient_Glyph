using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Interactors.Interaction.Interfaces
{
    public interface IInteractable
    {
        public void InteractWith(HitInteraction hit);

        public void InteractWith(FunctionalInteraction func);

        public void InteractWith(ICollection<object> listItems);
    }
}