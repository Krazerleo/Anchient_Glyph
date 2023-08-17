using System;

using AncientGlyph.GameScripts.ModelElements.Interaction.Interfaces;
using AncientGlyph.GameScripts.ModelElements.EntityModels.Interfaces;
using AncientGlyph.GameScripts.ModelElements.Interaction;

namespace AncientGlyph.GameScripts.ModelElements.EntityModels
{
    public class CreatureEntityModel : IInteractable
    {
        public void Hit(IEntityModel entityModel) 
        {
            Interact(entityModel, new HitInteraction(10));
        }

        public void Interact(IEntityModel toEntity, IInteraction interaction)
        {
            interaction.Accept(toEntity);
        }

        public void InteractWith(HitInteraction hit)
        {
            Console.WriteLine($"GET HITTED BY {hit.DamageAmount}");
        }
    }
}