using AncientGlyph.GameScripts.GameSystems.InventorySystem;

namespace AncientGlyph.GameScripts.Serialization.SaveInfo
{
    public class InventoryInfo
    {
        public InventoryModel MainInventory;
        
        public InventoryModel ExtraInventoryLeft;
        public bool ExtraInventoryLeftEnabled;
        
        public InventoryModel ExtraInventoryRight;
        public bool ExtraInventoryRightEnabled;

        public InventoryInfo(InventoryModel mainInventory,
            InventoryModel extraInventoryLeft,
            bool extraInventoryLeftEnabled,
            InventoryModel extraInventoryRight,
            bool extraInventoryRightEnabled)
        {
            MainInventory = mainInventory;
            ExtraInventoryLeft = extraInventoryLeft;
            ExtraInventoryLeftEnabled = extraInventoryLeftEnabled;
            ExtraInventoryRight = extraInventoryRight;
            ExtraInventoryRightEnabled = extraInventoryRightEnabled;
        }
    }
    
    public readonly struct PlayerSaveInfo
    {
        public readonly InventoryInfo InventoryInfo;

        public PlayerSaveInfo(InventoryInfo inventoryInfo)
        {
            InventoryInfo = inventoryInfo;
        }
    }
}