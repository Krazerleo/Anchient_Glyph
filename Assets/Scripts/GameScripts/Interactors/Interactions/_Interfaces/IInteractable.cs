using System.Collections.Generic;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;

namespace AncientGlyph.GameScripts.Interactors.Interaction
{
    public interface IInteractable
    {
        public void AcceptInteraction(HitInteraction hit);

        public void AcceptInteraction(FunctionalInteraction func);

        public void AcceptInteraction(ICollection<GameItem> listItems);
    }
}