namespace AncientGlyph.GameScripts.Constants
{
    public static class GameConstants
    {
        public const int LevelCellsSizeX = 32;
        public const int LevelCellsSizeZ = 32;
        public const int LevelCellsSizeY = 6;

        public const int MaxPathFindingSteps = 128;

        /// <summary>
        /// Maximum size X of Item in Inventory measured in Grid Cell Sides
        /// </summary>
        public const int MaxItemSizeX = 4;

        /// <summary>
        /// Maximum size Y of Item in Inventory measured in Grid Cell Sides
        /// </summary>
        public const int MaxItemSizeY = 4;

        /// <summary>
        /// 6 cells width of main storage
        /// </summary>
        public const int MainStorageSizeX = 6;
        
        /// <summary>
        /// 6 cells height of main storage
        /// </summary>
        public const int MainStorageSizeY = 4;
        
        /// <summary>
        /// 3 cells width of side (left and right) storage
        /// </summary>
        public const int SideStorageSizeX = 6;
        
        /// <summary>
        /// 3 cells height of side (left and right) storages
        /// </summary>
        public const int SideStorageSizeY = 4;

        public const uint UndefinedItemId = 9999999;

        public const string PlayerName = "Player";
        public const string PlayerId = "PLAYERID";

        public const int MaxSaveSlotNumber = 3;
    }
}