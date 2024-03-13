using AncientGlyph.GameScripts.Interactors.Entities.Controller;

namespace AncientGlyph.GameScripts.Interactors.Interactions
{
    public class HitInteraction : IInteraction
    {
        public int DamageAmount { get; private set; }

        public HitInteraction(int damageAmount)
        {
            DamageAmount = damageAmount;
        }

        public void InteractTo(IEntityController entityModel)
        {
            entityModel.AcceptInteraction(this);
        }
    }
}