using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Interactors.Interaction.Interfaces
{
    public interface IInteractable
    {
        public void AcceptInteraction(HitInteraction hit);

        public void AcceptInteraction(FunctionalInteraction func);

        public void AcceptInteraction(ICollection<object> listItems);
    }
}