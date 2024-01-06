using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.Interactors.Interaction
{
    public class FunctionalInteraction : IInteraction
    {
        public void InteractTo(IEntityModel entityModel)
        {
            entityModel.AcceptInteraction(this);
        }
    }
}