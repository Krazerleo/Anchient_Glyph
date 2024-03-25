using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class InventoryView
    {
        private readonly struct InventoryPosition
        {
            public readonly Vector2Int SlotModelPosition;
            public readonly Vector2 SlotViewPosition;
            public readonly InventoryModel CurrentInventory;

            public InventoryPosition(Vector2 slotViewPosition,
                Vector2Int slotModelPosition,
                InventoryModel inventory)
            {
                SlotViewPosition = slotViewPosition;
                SlotModelPosition = slotModelPosition;
                CurrentInventory = inventory;
            }
        }
    }
}