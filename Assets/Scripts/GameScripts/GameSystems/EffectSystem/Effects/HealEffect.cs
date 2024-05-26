using System;
using AncientGlyph.GameScripts.EntityModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects
{
    [Serializable]
    public class HealEffect : IEffect
    {
        public int HealthPointsCount;

        public int GetPower() => HealthPointsCount;

        public void ApplyOn(IEffectAcceptor entity)
        {
            //TODO : Make Effects
            Debug.Log("TODO: Make Heal Effect");
        }
    }
}