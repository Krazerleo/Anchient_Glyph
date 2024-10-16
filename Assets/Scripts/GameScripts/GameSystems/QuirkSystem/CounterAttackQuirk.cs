using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;

namespace AncientGlyph.GameScripts.GameSystems.QuirkSystem
{
    public class CounterAttackQuirk : IReactiveQuirk
    {
        public void ApplyTo(ReactionDispatcher dispatcher) => dispatcher.AddAfterGettingDamageHook(this);

        public bool Precondition() => true;

        public (IEffect effect, ReactiveQuirkTarget target) ExecuteReaction(IEffect appliedEffect, PlayerModel playerModel)
        {
            if (appliedEffect is DamageEffect)
            {
                // TODO: Implement calculation of damage strength of Player
                return (new DamageEffect(), ReactiveQuirkTarget.Source);
            }

            return (new NullEffect(), ReactiveQuirkTarget.None);
        }
    }
}