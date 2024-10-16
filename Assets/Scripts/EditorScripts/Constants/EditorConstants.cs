using AncientGlyph.GameScripts.Constants;

namespace AncientGlyph.EditorScripts.Constants
{
    public static class EditorConstants
    {
        public const float DistanceTolerance = 0.01f;

        public const float GridSizeX = GameConstants.LevelCellsSizeX / 10f - DistanceTolerance;
        public const float GridSizeZ = GameConstants.LevelCellsSizeZ / 10f - DistanceTolerance;
        public const float GridSizeY = GameConstants.LevelCellsSizeY / 10f - DistanceTolerance;

        public const float FloorHeight = 1.5f;
        public const float FloorSizeX = 1.0f;
        public const float FloorSizeZ = 1.0f;
    }
}