using System.Collections.Generic;
using AncientGlyph.GameScripts.GameSystems.InventorySystem;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using NUnit.Framework;
using UnityEngine;

namespace AncientGlyph.TestScripts.ModelTests
{
    [TestFixture]
    public class StorageTests
    {
        [Test]
        public void InventoryModel_TEST_PLACE_ITEM()
        {
            var inventoryModel = new Storage(2, 2);

            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 1),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
            });

            Assert.IsTrue(inventoryModel.CanItemBePlaced(0, 0, gameItem, 0));
        }
        
        [Test]
        public void InventoryModel_TEST_CAN_NOT_PLACE_ITEM()
        {
            var inventoryModel = new Storage(2, 2);

            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 1),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
            });
            
            var gameItemObstacle = ScriptableObject.CreateInstance<GameItem>();
            gameItemObstacle.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
            });

            Assert.IsTrue(inventoryModel.TryPlaceItemToPosition(1, 1, gameItemObstacle, 0));
            Assert.IsFalse(inventoryModel.CanItemBePlaced(0, 0, gameItem, 0));
        }
        
        [Test]
        public void InventoryModel_TEST_CAN_PLACE_ITEM_DENSE_PACKING()
        {
            var inventoryModel = new Storage(2, 2);

            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
            });
            
            var gameItemObstacle = ScriptableObject.CreateInstance<GameItem>();
            gameItemObstacle.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
            });

            Assert.IsTrue(inventoryModel.TryPlaceItemToPosition(1, 1, gameItemObstacle, 0));
            Assert.IsTrue(inventoryModel.CanItemBePlaced(0, 0, gameItem, 0));
        }
        
        [Test]
        public void InventoryModel_TEST_CAN_PLACE_ITEM_AND_REMOVE_AND_PLACE_AGAIN()
        {
            var inventoryModel = new Storage(2, 2);

            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
            });
            
            Assert.IsTrue(inventoryModel.TryPlaceItemToPosition(0, 0, gameItem, 0));
            Assert.IsTrue(inventoryModel.TryTakeItemFromPosition(0, 0, out _));
            Assert.IsTrue(inventoryModel.TryPlaceItemToPosition(0, 0, gameItem, 0));
        }
        
        [Test]
        public void InventoryModel_TEST_CANNOT_PLACE_ITEM_OUT_OF_BORDER()
        {
            var inventoryModel = new Storage(2, 2);

            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.CellSet = new CellSet(new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
            });
            
            Assert.IsFalse(inventoryModel.TryPlaceItemToPosition(1, 1, gameItem, 0));
        }
    }
}