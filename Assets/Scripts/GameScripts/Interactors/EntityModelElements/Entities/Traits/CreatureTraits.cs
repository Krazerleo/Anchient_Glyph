using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.FightingSystem;
using AncientGlyph.GameScripts.Interactors.Entities.Controllers;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities.Traits
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
        public int DamageStrength;
        public DamageType DamageType;

        [Header("Behaviour")]
        public BehaviourType BehaviourType;
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