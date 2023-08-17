using AncientGlyph.GameScripts.ModelElements.EntityModels.Interfaces;

namespace AncientGlyph.GameScripts.ModelElements.Interaction.Interfaces
{
    public interface IInteraction
    {
        public void Accept(IEntityModel entityModel);
    }
}