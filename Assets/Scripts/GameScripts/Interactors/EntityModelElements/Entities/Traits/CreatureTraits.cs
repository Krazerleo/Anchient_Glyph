using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Enums;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    [CreateAssetMenu(fileName = "Creature Traits Config", menuName = ProjectConstants.ScriptableObjectAssetMenuName)]
    public class CreatureTraits : ScriptableObject
    {
        [SerializeField]
        public string CreatureName;

        [SerializeField]
        public string Description;

        [Header("Health Points / Magic Points")]

        [SerializeField]
        public int MaxHealthPoints;

        [SerializeField]
        public int MaxManaPoints;

        [Header("Damage Traits")]

        [SerializeField]
        public int DamageStrength;

        [SerializeField]
        public DamageType DamageType;

        [Header("Movement")]

        [SerializeField]
        public int MovementType;

        [SerializeField]
        public int MovementSteps;

        [SerializeField]
        public bool IsFullSize;

        [Header("Resistance")]

        [SerializeField]
        public int PhysicalResistance;

        [SerializeField]
        public int FireResistance;

        [SerializeField]
        public int WaterResistance;

        [SerializeField]
        public int EarthResistance;

        [SerializeField]
        public int AirResistance;
    }
}