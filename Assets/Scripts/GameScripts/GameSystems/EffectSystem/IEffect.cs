using AncientGlyph.GameScripts.EntityModel;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem
{
    public interface IEffect
    {
        public void ApplyOn(IEffectAcceptor entity);
    }
}