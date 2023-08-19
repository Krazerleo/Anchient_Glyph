using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    public class CreatureTraits : ScriptableObject
    {
        [Header("Creature Attributes")]

        [SerializeField]
        public int MaxHealthPoints;

        [SerializeField]
        public int DamageStrength;

        [SerializeField]
        public int MovementType;

        public CreatureTraits()
        {

        }
    }
}