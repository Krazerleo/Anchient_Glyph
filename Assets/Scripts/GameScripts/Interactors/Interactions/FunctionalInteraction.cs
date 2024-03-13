using AncientGlyph.GameScripts.Interactors.Entities;

namespace AncientGlyph.GameScripts.Interactors.Interactions
{
    public class FunctionalInteraction : IInteraction
    {
        public void InteractTo(IEntityModel entityModel)
        {
            entityModel.AcceptInteraction(this);
        }
    }
}