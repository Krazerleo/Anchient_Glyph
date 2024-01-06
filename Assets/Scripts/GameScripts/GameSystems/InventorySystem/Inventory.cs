using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    public class Inventory
    {
        private List<InventoryCell> _inventoryCells = new();
        private Dictionary<uint, InventoryCell> _itemPivots = new();

        public Inventory(int width, int height)
        {
            _inventoryCells.Capacity = width*height;
        }

        public InventoryItem TakeItem(Vector2Int targetCell)
        {
            throw new NotImplementedException();
        }

        public bool TryPlaceItem(InventoryItem item, Vector2Int targetCell)
        {
            throw new NotImplementedException();
        }
    }
}