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
        public void AcceptInteraction(HitInteraction hit) { }

        public void AcceptInteraction(FunctionalInteraction func)
        {
            //TODO
        }

        public void AcceptInteraction(ICollection<object> listItems) { }
    }
}