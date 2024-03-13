using AncientGlyph.GameScripts.Interactors.Entities.Controller;

namespace AncientGlyph.GameScripts.Interactors.Interactions
{
    public class FunctionalInteraction : IInteraction
    {
        public void InteractTo(IEntityController entityModel)
        {
            entityModel.AcceptInteraction(this);
        }
    }
}