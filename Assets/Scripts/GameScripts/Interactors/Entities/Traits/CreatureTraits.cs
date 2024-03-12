using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.FightingSystem;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;
using UnityEngine;
using UnityEngine.Serialization;

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
        public int DamageStrength;
        public DamageType DamageType;

        [FormerlySerializedAs("BehaviourType")] [Header("Behaviour")]
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