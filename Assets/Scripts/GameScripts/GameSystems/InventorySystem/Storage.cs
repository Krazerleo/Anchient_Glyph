using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class Storage : IXmlSerializable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Id { get; private set; }
        private readonly List<InventoryCell> _inventoryCells = new();

        private InventoryCell this[int x, int y]
        {
            get => _inventoryCells[x + y * Width];
            set => _inventoryCells[x + y * Width] = value;
        }

        private const int EmptyCellPivotIndicator = -1;
        private readonly Dictionary<Vector2Int, GameItem> _itemPivots = new();

        public int SlotsCount => _inventoryCells.Count;

        /// <summary>
        /// Only for serialization
        /// </summary>
        public Storage() {}
        
        public Storage(int width, int height)
        {
            PopulateInventory(width, height);
        }

        public Storage(Vector2Int size) : this(size.x, size.y) {}

        private void PopulateInventory(int width, int height)
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

        public bool TryPlaceItemToPosition(Vector2Int position, GameItem item, int rotations)
        {
            if (CanItemBePlaced(position, item, rotations) == false)
            {
                return false;
            }

            foreach (Vector2Int itemCell in item.CellSet.GetRotatedGeometry(rotations).GetDefinedGeometry())
            {
                Vector2Int addedCellPosition = new(position.x + itemCell.x, position.y + itemCell.y);

                InventoryCell addedCell = this[addedCellPosition.x, addedCellPosition.y];
                addedCell.ItemId = item.ItemId;
                addedCell.ItemPivotCell = position;
                this[addedCellPosition.x, addedCellPosition.y] = addedCell;
            }

            _itemPivots.Add(position, item);

            return true;
        }

        public bool TryPlaceItemToPosition(int xPosition, int yPosition, GameItem item, int rotations) =>
            TryPlaceItemToPosition(new Vector2Int(xPosition, yPosition), item, rotations);

        public bool CanItemBePlaced(Vector2Int position, GameItem item, int rotations)
        {
            foreach (Vector2Int itemCell in item.CellSet.GetRotatedGeometry(rotations).GetDefinedGeometry())
            {
                Vector2Int addedCellPosition = new(position.x + itemCell.x, position.y + itemCell.y);

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

        public bool CanItemBePlaced(int xPosition, int yPosition, GameItem item, int rotations) =>
            CanItemBePlaced(new Vector2Int(xPosition, yPosition), item, rotations);

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            Width = int.Parse(reader.GetAttribute($"{nameof(Width)}")!);
            Height = int.Parse(reader.GetAttribute($"{nameof(Height)}")!);
            Id = reader.GetAttribute($"{nameof(Id)}");
            
            PopulateInventory(Width, Height);
            
            reader.ReadStartElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement($"{nameof(Storage)}");
            writer.WriteAttributeString($"{nameof(Id)}", Id);
            writer.WriteAttributeString($"{nameof(Width)}", Width.ToString());
            writer.WriteAttributeString($"{nameof(Height)}", Height.ToString());

            foreach ((Vector2Int position, GameItem item) in _itemPivots)
            {
                writer.WriteStartElement("Item");
                
                writer.WriteAttributeString("Position", position.ToString());
                writer.WriteAttributeString("Id", item.ItemId.ToString());
                
                writer.WriteEndElement();
            }
            
            writer.WriteEndElement();
        }
    }
}