using System.Collections.Generic;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
{
    public class ApplyEffectsAction
    {
        private readonly IEnumerable<IEffect> _targetEffects;
        private readonly IEnumerable<IEffect> _selfEffects;
        
        public ApplyEffectsAction(IEnumerable<IEffect> targetEffects, IEnumerable<IEffect> selfEffects)
        {
            _targetEffects = targetEffects;
            _selfEffects = selfEffects;
        }

        public void Execute(IEffectAcceptor self, IEffectAcceptor target)
        {
            foreach (var selfEffect in _selfEffects)
            {
                selfEffect.ApplyOn(self);
            }

            foreach (var targetEffect in _targetEffects)
            {
                targetEffect.ApplyOn(target);
            }
        }
    }
}