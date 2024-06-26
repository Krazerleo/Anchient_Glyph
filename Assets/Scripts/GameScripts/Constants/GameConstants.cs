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

        public const uint UndefinedItemId = 9999999;

        public const string PlayerName = "Player";
        public const string PlayerId = "PLAYERID";
        
        public const int MaxSaveSlotNumber = 3;
    }
}