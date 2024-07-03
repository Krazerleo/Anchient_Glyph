using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class InventoryModel
    {
        private struct InventoryCell
        {
            public uint ItemId { get; set; }
            public Vector2Int ItemPivotCell { get; set; }
        }
    }
}