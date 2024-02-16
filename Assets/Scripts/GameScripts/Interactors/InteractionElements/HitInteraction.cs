namespace AncientGlyph.GameScripts.Interactors.Interaction
{
    public class HitInteraction : IInteraction
    {
        public int DamageAmount { get; private set; }

        public HitInteraction(int damageAmount)
        {
            DamageAmount = damageAmount;
        }

        public void InteractTo(IEntityModel entityModel)
        {
            entityModel.AcceptInteraction(this);
        }
    }
}