using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.InventorySystem;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using UnityEngine;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.InventoryInstaller
{
    public class DevInventoryInstaller : MonoInstaller<DevInventoryInstaller>
    {
        public List<GameItem> GameItemsInMainInventory;
        public List<GameItem> GameItemsInLeftInventory;
        public List<GameItem> GameItemsInRightInventory;

        public override void InstallBindings()
        {
            StorageFactory storageFactory = new();
            (Storage storage, bool success) main =
                storageFactory.CreateStorageWithItems(GameItemsInMainInventory,
                                                      new Vector2Int(GameConstants.MainStorageSizeX,
                                                                     GameConstants.MainStorageSizeY));

            if (main.success == false)
            {
                Debug.LogWarning("Cannot place all items in main inventory");
            }
            
            (Storage storage, bool success) left =
                storageFactory.CreateStorageWithItems(GameItemsInLeftInventory,
                                                      new Vector2Int(GameConstants.SideStorageSizeX,
                                                                     GameConstants.SideStorageSizeX));
            
            if (left.success == false)
            {
                Debug.LogWarning("Cannot place all items in left inventory");
            }
            
            (Storage storage, bool success) right =
                storageFactory.CreateStorageWithItems(GameItemsInRightInventory,
                                                      new Vector2Int(GameConstants.SideStorageSizeX,
                                                                     GameConstants.SideStorageSizeX));
            
            if (right.success == false)
            {
                Debug.LogWarning("Cannot place all items in right inventory");
            }

            InventoryModel playerInventory = new(main.storage, left.storage, right.storage);

            Container.BindInterfacesAndSelfTo<InventoryModel>()
                     .FromInstance(playerInventory);
        }
    }
}