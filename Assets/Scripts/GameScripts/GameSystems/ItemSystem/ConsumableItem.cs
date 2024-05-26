using System.Collections.Generic;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    public class ConsumableItem : GameItem
    {
        [SerializeReference]
        public List<IEffect> Effects;

        public void ApplyEffect(PlayerModel player)
        {
            
        }
    }
}