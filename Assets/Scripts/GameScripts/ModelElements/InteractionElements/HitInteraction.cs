using AncientGlyph.GameScripts.ModelElements.EntityModels.Interfaces;
using AncientGlyph.GameScripts.ModelElements.Interaction.Interfaces;

namespace AncientGlyph.GameScripts.ModelElements.Interaction
{
    public class HitInteraction : IInteraction
    {
        public int DamageAmount { get; private set; }

        public HitInteraction(int damageAmount)
        {
            DamageAmount = damageAmount;
        }

        public void Accept(IEntityModel entityModel)
        {
            entityModel.InteractWith(this);
        }
    }
}