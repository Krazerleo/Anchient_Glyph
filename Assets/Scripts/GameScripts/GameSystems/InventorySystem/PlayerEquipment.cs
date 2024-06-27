using System.Collections.Generic;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Services.LoggingService;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class PlayerEquipment
    {
        private readonly FreeEquipSlot _freeEquipSlot;
        private readonly ILoggingService _logger;
        private readonly TwoHandedOccupiedSlot _twoHandedOccupiedSlot;
        private Dictionary<(EquipableItemType, object), EquipableItem> _equipableSlots;

        public PlayerEquipment(ILoggingService logger)
        {
            _logger = logger;
            _freeEquipSlot = ScriptableObject.CreateInstance<FreeEquipSlot>();
            _twoHandedOccupiedSlot = ScriptableObject.CreateInstance<TwoHandedOccupiedSlot>();
            
            InitializeEquipmentCollection();
        }

        public bool TrySetItemInSlot(EquipableItem item, EquipableItemType selectedSlot,
            HandWeaponSelector whichHandSlot = HandWeaponSelector.None,
            TrinketSelector whichTrinketSlot = TrinketSelector.None,
            bool pushItemToSlot = false)
        {
            if (item.Type != selectedSlot)
            {
                _logger.LogTrace($"Item with type {item.Type} is incompatible " +
                                 $"to slot with type {selectedSlot}");

                return false;
            }

            switch (item.Type)
            {
                case EquipableItemType.Weapon:
                    if (whichHandSlot == HandWeaponSelector.None)
                    {
                        _logger.LogWarning("Specify whatHand parameter");
                    }

                    EquipableItem weaponSlot = _equipableSlots[(EquipableItemType.Weapon, whichHandSlot)];

                    if (weaponSlot is not FreeEquipSlot)
                    {
                        _logger.LogTrace($"Tried to push weapon {item.Name} to {whichHandSlot}: " +
                                         $"slot already occupied by {weaponSlot.Name}");
                        return false;
                    }

                    if (pushItemToSlot)
                    {
                        _equipableSlots[(EquipableItemType.Weapon, whichHandSlot)] = item;
                        _logger.LogTrace($"Pushed weapon {item.Name} to weapon slot {whichHandSlot}");
                        return true;
                    }

                    return false;

                case EquipableItemType.Trinket:
                    if (whichTrinketSlot == TrinketSelector.None)
                    {
                        _logger.LogWarning("Specify whatTrinket parameter");
                    }

                    EquipableItem trinketSlot = _equipableSlots[(EquipableItemType.Trinket, whichTrinketSlot)];

                    if (trinketSlot is not FreeEquipSlot)
                    {
                        _logger.LogTrace($"Tried to push trinket {item.Name} to {whichTrinketSlot}: " +
                                         $"slot is already occupied by {trinketSlot.Name}");
                        return false;
                    }

                    if (pushItemToSlot)
                    {
                        _equipableSlots[(EquipableItemType.Trinket, whichTrinketSlot)] = item;
                        _logger.LogTrace($"Pushed trinket {item.Name} to trinket slot {whichTrinketSlot}");
                        return true;
                    }

                    return false;

                case EquipableItemType.TwoHandedWeapon:
                    EquipableItem leftHandSlot =
                        _equipableSlots[(EquipableItemType.Weapon, HandWeaponSelector.LeftHand)];
                    EquipableItem rightHandSlot =
                        _equipableSlots[(EquipableItemType.Weapon, HandWeaponSelector.RightHand)];

                    if ((leftHandSlot is FreeEquipSlot && rightHandSlot is FreeEquipSlot) == false)
                    {
                        _logger.LogTrace($"Tried to push two handed weapon {item.Name} to both hand: " +
                                         $"slot is already occupied by {leftHandSlot.Name} or {rightHandSlot.Name}");
                        return false;
                    }

                    if (pushItemToSlot)
                    {
                        _equipableSlots[(EquipableItemType.Weapon, HandWeaponSelector.LeftHand)] = item;
                        _equipableSlots[(EquipableItemType.Weapon, HandWeaponSelector.RightHand)] =
                            _twoHandedOccupiedSlot;
                        _logger.LogTrace($"Pushed two handed weapon {item.Name} to both hands");
                        return true;
                    }

                    return false;

                case EquipableItemType.Helmet:
                case EquipableItemType.BodyArmor:
                case EquipableItemType.Pants:
                case EquipableItemType.Boots:
                case EquipableItemType.Gauntlets:
                case EquipableItemType.Necklace:
                    EquipableItem equipableItem = _equipableSlots[(item.Type, null)];

                    if (equipableItem is not FreeEquipSlot)
                    {
                        _logger.LogTrace($"Tried to push {item.Type} with name: {item.Name}: " +
                                         $"slot is already occupied by {equipableItem.Name}");
                        return false;
                    }

                    if (pushItemToSlot)
                    {
                        _logger.LogTrace($"Pushed item {item.Type} with name {item.Name}");
                        _equipableSlots[(item.Type, null)] = item;
                        return true;
                    }

                    return false;

                default:
                    _logger.LogError($"Unexpected equipment item with type {item.Type}");
                    return false;
            }
        }

        private void InitializeEquipmentCollection()
        {
            _equipableSlots = new Dictionary<(EquipableItemType, object), EquipableItem>
            {
                { (EquipableItemType.Weapon, HandWeaponSelector.LeftHand), _freeEquipSlot },
                { (EquipableItemType.Weapon, HandWeaponSelector.RightHand), _freeEquipSlot },
                { (EquipableItemType.Trinket, TrinketSelector.LeftHand), _freeEquipSlot },
                { (EquipableItemType.Trinket, TrinketSelector.RightHand), _freeEquipSlot },

                { (EquipableItemType.Helmet, null), _freeEquipSlot },
                { (EquipableItemType.BodyArmor, null), _freeEquipSlot },
                { (EquipableItemType.Pants, null), _freeEquipSlot },
                { (EquipableItemType.Boots, null), _freeEquipSlot },
                { (EquipableItemType.Gauntlets, null), _freeEquipSlot },
                { (EquipableItemType.Necklace, null), _freeEquipSlot },
            };
        }
    }
}