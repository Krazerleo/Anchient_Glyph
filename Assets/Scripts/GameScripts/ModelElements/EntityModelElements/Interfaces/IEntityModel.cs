using AncientGlyph.GameScripts.ModelElements.Interaction.Interfaces;

namespace AncientGlyph.GameScripts.ModelElements.EntityModels.Interfaces
{
    public interface IEntityModel : IInteractable
    {
        public void Interact(IEntityModel toEntity, IInteraction interaction);
    }
}