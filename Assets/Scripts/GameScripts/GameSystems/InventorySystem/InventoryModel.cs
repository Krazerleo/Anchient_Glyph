using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class InventoryModel
    {
        private struct InventoryCell
        {
            public uint ItemId { get; set; }
            public int PivotX { get; set; }
            public int PivotY { get; set; }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        private readonly List<InventoryCell> _inventoryCells = new();

        private InventoryCell this[int x, int y]
            => _inventoryCells[x + y * Width];

        private const int EmptyCellPivotIndicator = -1;
        private Dictionary<(int x, int y), GameItem> _itemPivots = new();

        public int SlotsCount => _inventoryCells.Count;

        public InventoryModel(int width, int height)
        {
            Width = width;
            Height = height;
            _inventoryCells.Capacity = width * height;

            for (int i = 0; i < _inventoryCells.Capacity; i++)
            {
                _inventoryCells.Add(new InventoryCell()
                {
                    PivotX = 0,
                    PivotY = 0,
                    ItemId = Constants.GameConstants.UndefinedItemId,
                });
            }
        }

        public bool TryTakeItemFromPosition(int xPosition, int yPosition, out GameItem gameItem)
        {
            var pivotCell = this[xPosition, yPosition];

            if (pivotCell.PivotX == EmptyCellPivotIndicator)
            {
                gameItem = null;
                return false;
            }

            var requestedItem = _itemPivots[(pivotCell.PivotX, pivotCell.PivotY)];
            gameItem = requestedItem;

            foreach (var cell in gameItem.CellSet.GetDefinedGeometry())
            {
                var removingCell = this[xPosition + cell.x, yPosition + cell.y];
                removingCell.PivotX = -1;
                removingCell.PivotY = -1;
                removingCell.ItemId = Constants.GameConstants.UndefinedItemId;
            }

            return true;
        }

        public bool TryPlaceItemToPosition(int xPosition, int yPosition, GameItem item)
        {
            if (CanItemPlaced(xPosition, yPosition, item) == false)
            {
                return false;
            }

            var pivotCell = item.CellSet.GetDefinedGeometry().First();

            foreach (var itemCell in item.CellSet.GetDefinedGeometry())
            {
                var addedCell = this[xPosition + itemCell.x, yPosition + itemCell.y];
                addedCell.ItemId = item.ItemId;
                addedCell.PivotX = pivotCell.x;
                addedCell.PivotY = pivotCell.y;
            }

            return true;
        }

        public bool CanItemPlaced(int xPosition, int yPosition, GameItem item)
        {
            foreach (var itemCell in item.CellSet.GetDefinedGeometry())
            {
                var checkedCell = this[xPosition + itemCell.x, yPosition + itemCell.y];

                if (checkedCell.ItemId != Constants.GameConstants.UndefinedItemId)
                {
                    return false;
                }
            }

            return true;
        }
    }
}