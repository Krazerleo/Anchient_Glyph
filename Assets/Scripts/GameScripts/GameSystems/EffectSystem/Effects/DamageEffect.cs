using System;
using AncientGlyph.GameScripts.EntityModel;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects
{
    [Serializable]
    public class DamageEffect : IEffect
    {
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