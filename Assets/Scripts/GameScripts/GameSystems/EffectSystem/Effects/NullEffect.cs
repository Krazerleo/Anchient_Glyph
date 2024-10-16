using AncientGlyph.GameScripts.EntityModel;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects
{
    public class NullEffect : IEffect
    {
        public int GetPower() => 0;

        public void ApplyOn(IEffectAcceptor entity) { }
    }
}