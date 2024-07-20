using AncientGlyph.GameScripts.GameSystems.InventorySystem;

namespace AncientGlyph.GameScripts.Serialization.SaveInfo
{
    public class InventoryInfo
    {
        public Storage MainInventory;
        
        public Storage ExtraInventoryLeft;
        public bool ExtraInventoryLeftEnabled;
        
        public Storage ExtraInventoryRight;
        public bool ExtraInventoryRightEnabled;

        public InventoryInfo(Storage mainInventory,
            Storage extraInventoryLeft,
            bool extraInventoryLeftEnabled,
            Storage extraInventoryRight,
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