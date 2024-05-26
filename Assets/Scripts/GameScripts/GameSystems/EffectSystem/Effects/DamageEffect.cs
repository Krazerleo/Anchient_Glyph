using System;
using AncientGlyph.GameScripts.EntityModel;
using UnityEngine;

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
            // TODO: Make Effects
            Debug.Log("TODO: Make Damage Effect");
        }
    }
}