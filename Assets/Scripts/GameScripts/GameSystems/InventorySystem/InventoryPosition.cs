using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class InventoryView
    {
        private readonly struct InventoryPosition
        {
            public readonly Vector2Int SlotModelPosition;
            public readonly Storage ParentInventory;

            public InventoryPosition(Vector2Int slotModelPosition, Storage inventory)
            {
                SlotModelPosition = slotModelPosition;
                ParentInventory = inventory;
            }
        }
    }
}