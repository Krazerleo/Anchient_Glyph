using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;

namespace AncientGlyph.GameScripts.EntityModel
{
    public interface IEffectAcceptor
    {
        void AcceptDamageEffect(DamageEffect damageEffect);
    }
}