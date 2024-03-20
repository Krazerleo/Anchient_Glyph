using AncientGlyph.GameScripts.EntityModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects
{
    public class GoToEffect : IEffect
    {
        public readonly Vector3Int Offset;
        
        public GoToEffect(Vector3Int offset)
        {
            Offset = offset;
        }
        
        public int GetPower()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyOn(IEffectAcceptor entity)
        {
            entity.AcceptGoToEffect(this);
        }
    }
}