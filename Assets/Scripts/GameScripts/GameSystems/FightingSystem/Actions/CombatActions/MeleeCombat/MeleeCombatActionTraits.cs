using AncientGlyph.GameScripts.Constants;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.CombatActions.MeleeCombat
{
    [CreateAssetMenu(
        fileName = "Melee Combat Action",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Melee Combat Action")]
    public class MeleeCombatActionTraits : ScriptableObject
    {
        public string ActionName;
        
        [Header("Attack Parameters")] 
        
        public DamageType DamageType;
        public int DamageHitValue;
    }
}