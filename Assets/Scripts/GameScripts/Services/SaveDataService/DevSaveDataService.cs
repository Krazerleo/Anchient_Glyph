using System;
using AncientGlyph.GameScripts.GameSystems.InventorySystem;
using AncientGlyph.GameScripts.Serialization.SaveInfo;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.SaveDataService
{
    [UsedImplicitly]
    public class DevSaveDataService : ISaveDataService
    {
        public BaseSaveInfo BaseInfo { get; }
        public PlayerSaveInfo PlayerInfo { get; }

        public DevSaveDataService(Vector3Int playerPosition)
        {
            BaseInfo = new BaseSaveInfo(playerPosition, "", DateTime.Now, null, "");
            PlayerInfo = CreateDevPlayerSaveInfo();
        }

        private PlayerSaveInfo CreateDevPlayerSaveInfo()
        {
            var mainInventory = new InventoryModel(6, 4);
            var leftInventory = new InventoryModel(3, 3);
            var rightInventory = new InventoryModel(3, 3);

            var inventorySaveInfo = new InventoryInfo(mainInventory, leftInventory, false, rightInventory, false);

            return new PlayerSaveInfo(inventorySaveInfo);
        }
        
        public void LoadSaveFile(string saveFilePath)
        {
        }
    }
}