using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Traits
{
    [CreateAssetMenu(
        fileName = "Creature Traits Config",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Creature Traits Config")]
    public class CreatureTraits : ScriptableObject
    {
        public string Name;
        public string Description;

        [Header("Health Points / Magic Points")]
        public int MaxHealthPoints;
        public int MaxManaPoints;

        [Header("Damage Traits")]
        public List<string> ActionNames;

        [Header("Behaviour")] 
        public MovementType MovementType;
        public int MovementSteps;
        public bool IsFullSize;

        [Header("Resistance")] 
        public int PhysicalResistance;
        public int FireResistance;
        public int WaterResistance;
        public int EarthResistance;
        public int AirResistance;
    }
}