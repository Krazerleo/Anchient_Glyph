using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Traits
{
    [CreateAssetMenu(
        fileName = "Creature Traits Config",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Creature Traits Config")]
    public class CreatureTraits : ScriptableObject
    {
        public string Name;
        public string Description;
        public bool IsFullSize;

        [Header("Health Points / Magic Points")]
        public int MaxHealthPoints;
        public int MaxManaPoints;

        [Header("Actions")]
        public List<ActionConfig> Actions;
        
        [Header("Moving Traits")] 
        public MovementType MovementType;
        public int MovementSteps;

        [Header("Resistance")] 
        public int PhysicalResistance;
        public int FireResistance;
        public int WaterResistance;
        public int EarthResistance;
        public int AirResistance;
    }
}