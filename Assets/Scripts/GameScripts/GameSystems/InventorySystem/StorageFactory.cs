using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class StorageFactory
    {
        /// <summary>
        /// Simple greedy algorithm to create and populate storage with given items.
        /// Not guaranteed that all items can be fitted into it.
        /// </summary>
        /// <param name="items">Items to put into storage</param>
        /// <param name="storageSize">Size of storage</param>
        /// <returns>storage: created storage | success: true if all items were put into it </returns>
        public (Storage storage, bool success) CreateStorageWithItems(
            IEnumerable<GameItem> items, Vector2Int storageSize)
        {
            Storage resultStorage = new(storageSize);
            
            Vector2Int currentPointer = new(0,0);
            int maxHeightOnRow = 0;

            using IEnumerator<GameItem> itemEnumerator = items.GetEnumerator();
            
            while (itemEnumerator.Current != null)
            {
                GameItem item = itemEnumerator.Current;
                
                if (currentPointer.y > storageSize.y)
                {
                    return (resultStorage, false);
                }
                
                RectInt boundingBox = item.CellSet.GetBoundingBox();
                
                if (currentPointer.x + boundingBox.x < storageSize.x)
                {
                    if (currentPointer.y + boundingBox.y < storageSize.y)
                    {
                        maxHeightOnRow = Math.Max(maxHeightOnRow, boundingBox.y);
                        resultStorage.TryPlaceItemToPosition(currentPointer, item, 0);
                        currentPointer.x += boundingBox.x;
                        itemEnumerator.MoveNext();
                    }
                }
                else
                {
                    currentPointer.y += maxHeightOnRow;
                    currentPointer.x = 0;
                }
            }

            return (resultStorage, true);
        }
    }
}