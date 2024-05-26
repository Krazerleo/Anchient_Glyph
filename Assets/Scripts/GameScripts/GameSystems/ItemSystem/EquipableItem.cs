using System.Collections.Generic;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    public enum EquipableItemType
    {
        Weapon,
        TwoHandedWeapon,
        Helmet,
        BodyArmor,
        Pants,
        Boots,
        Gauntlets,
        Trinket,
        Necklace,
    }

    public enum HandWeaponSelector
    {
        None,
        LeftHand,
        RightHand,
    }

    public enum TrinketSelector
    {
        None,
        LeftHand,
        RightHand,
    }

    public class EquipableItem : GameItem
    {
        [SerializeReference]
        public List<IEffect> PersistentEffects;

        [field: SerializeField]
        public EquipableItemType Type { get; private set; }

        public void EquipItem(PlayerModel player) { }

        public void UnequipItem(PlayerModel player) { }

        public static EquipableItem CreateInstance(EquipableItemType type)
        {
            EquipableItem resultItem = ScriptableObject.CreateInstance<EquipableItem>();
            resultItem.Type = type;

            return resultItem;
        }
    }

    public class FreeEquipSlot : EquipableItem { }

    public class TwoHandedOccupiedSlot : EquipableItem { }
}