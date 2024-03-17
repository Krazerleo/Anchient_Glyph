using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.CombatActions.MeleeCombat;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Traits
{
    [CreateAssetMenu(
        fileName = "Creature Traits Config",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Creature Traits Config")]
    public class CreatureTraits : ScriptableObject
    {
        private static readonly HashSet<Type> ActionTypes = new()
        {
            typeof(MeleeCombatActionTraits),
        };
        
        public string Name;
        public string Description;
        public bool IsFullSize;

        [Header("Health Points / Magic Points")]
        public int MaxHealthPoints;
        public int MaxManaPoints;

        [Header("Damage Traits")]
        public List<ScriptableObject> ActionNames;

        [Header("Moving Traits")] 
        public MovementType MovementType;
        public int MovementSteps;

        [Header("Resistance")] 
        public int PhysicalResistance;
        public int FireResistance;
        public int WaterResistance;
        public int EarthResistance;
        public int AirResistance;

        public void OnValidate()
        {
            foreach (var action in ActionNames)
            {
                if (action == null)
                {
                    continue;
                }
                
                if (ActionTypes.Contains(action.GetType()) == false)
                {
                    Debug.LogError($"Unexpected action trait of type {action.GetType()} encountered\n" +
                                   "Check it out or add it to expected action traits types");
                    
                    return;
                }
            }
        }
    }
}