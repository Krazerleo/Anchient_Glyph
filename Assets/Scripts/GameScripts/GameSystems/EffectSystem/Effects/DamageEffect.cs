using System;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.CombatActions;
using Sirenix.OdinInspector;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects
{
    [Serializable]
    public class DamageEffect : IEffect
    {
        [MinValue(0)]
        public int Value;

        public DamageType Type;

        public int GetPower()
        {
            return Value;
        }

        public void ApplyOn(IEffectAcceptor entity)
        {
            throw new NotImplementedException();
        }
    }
}