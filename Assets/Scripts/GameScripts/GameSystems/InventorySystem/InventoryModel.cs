using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class InventoryModel
    {
        public int Width { get; }
        public int Height { get; }
        private readonly List<InventoryCell> _inventoryCells = new();

        private InventoryCell this[int x, int y]
        {
            get => _inventoryCells[x + y * Width];
            set => _inventoryCells[x + y * Width] = value;
        }

        private const int EmptyCellPivotIndicator = -1;
        private readonly Dictionary<Vector2Int, GameItem> _itemPivots = new();

        public int SlotsCount => _inventoryCells.Count;

        public InventoryModel(int width, int height)
        {
            Width = width;
            Height = height;
            _inventoryCells.Capacity = width * height;

            for (int i = 0; i < _inventoryCells.Capacity; i++)
            {
                _inventoryCells.Add(new InventoryCell
                {
                    ItemPivotCell = new Vector2Int(),
                    ItemId = Constants.GameConstants.UndefinedItemId,
                });
            }
        }

        public bool TryTakeItemFromPosition(int xPosition, int yPosition, out GameItem gameItem)
        {
            InventoryCell inventoryCell = this[xPosition, yPosition];

            if (inventoryCell.ItemPivotCell.x == EmptyCellPivotIndicator)
            {
                gameItem = null;
                return false;
            }

            GameItem requestedItem = _itemPivots[inventoryCell.ItemPivotCell];
            gameItem = requestedItem;

            foreach (Vector2Int cell in gameItem.CellSet.GetDefinedGeometry())
            {
                Vector2Int addedCellPosition = new(xPosition + cell.x, yPosition + cell.y);

                InventoryCell removingCell = this[addedCellPosition.x, addedCellPosition.y];
                removingCell.ItemPivotCell.Set(-1, -1);
                removingCell.ItemId = Constants.GameConstants.UndefinedItemId;
                this[addedCellPosition.x, addedCellPosition.y] = removingCell;
            }

            _itemPivots.Remove(inventoryCell.ItemPivotCell);

            return true;
        }

        public bool TryPlaceItemToPosition(int xPosition, int yPosition, GameItem item)
        {
            if (CanItemPlaced(xPosition, yPosition, item) == false)
            {
                return false;
            }

            Vector2Int pivotCell = item.CellSet.GetDefinedGeometry().First();

            foreach (Vector2Int itemCell in item.CellSet.GetDefinedGeometry())
            {
                Vector2Int addedCellPosition = new(xPosition + itemCell.x, yPosition + itemCell.y);

                InventoryCell addedCell = this[addedCellPosition.x, addedCellPosition.y];
                addedCell.ItemId = item.ItemId;
                addedCell.ItemPivotCell = pivotCell;
                this[addedCellPosition.x, addedCellPosition.y] = addedCell;
            }

            _itemPivots.Add(pivotCell, item);

            return true;
        }

        public bool CanItemPlaced(int xPosition, int yPosition, GameItem item)
        {
            foreach (Vector2Int itemCell in item.CellSet.GetDefinedGeometry())
            {
                Vector2Int addedCellPosition = new(xPosition + itemCell.x, yPosition + itemCell.y);

                if (addedCellPosition.x >= Width || addedCellPosition.x < 0 ||
                    addedCellPosition.y >= Height || addedCellPosition.y < 0)
                {
                    return false;
                }

                InventoryCell checkedCell = this[addedCellPosition.x, addedCellPosition.y];

                if (checkedCell.ItemId != Constants.GameConstants.UndefinedItemId)
                {
                    return false;
                }
            }

            return true;
        }

        private struct InventoryCell
        {
            public uint ItemId { get; set; }
            public Vector2Int ItemPivotCell { get; set; }
        }
    }
}