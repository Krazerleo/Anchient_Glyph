using AncientGlyph.GameScripts.EntityModel;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem
{
    public interface IEffect
    {
        int GetPower();
        
        void ApplyOn(IEffectAcceptor entity);
    }
}