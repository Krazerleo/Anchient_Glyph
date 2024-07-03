using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class InventoryView
    {
        private readonly struct InventoryPosition
        {
            public readonly Vector2Int SlotModelPosition;
            public readonly InventoryModel ParentInventory;

            public InventoryPosition(Vector2Int slotModelPosition, InventoryModel inventory)
            {
                SlotModelPosition = slotModelPosition;
                ParentInventory = inventory;
            }
        }
    }
}