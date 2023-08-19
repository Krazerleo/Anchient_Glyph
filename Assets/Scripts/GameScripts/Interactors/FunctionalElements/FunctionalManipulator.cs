using System.Collections.Generic;
using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;

namespace AncientGlyph.GameScripts.Interactors.Functional
{
    /// <summary>
    /// Buttons, levers, pull chains and so on.
    /// </summary>
    public class FunctionalManipulator : IInteractable
    {
        public void InteractWith(HitInteraction hit)
        {
            return;
        }

        public void InteractWith(FunctionalInteraction func)
        {
            //TODO
        }

        public void InteractWith(ICollection<object> listItems)
        {
            return;
        }
    }
}