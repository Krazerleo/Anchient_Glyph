using AncientGlyph.GameScripts.Constants;

namespace AncientGlyph.GameScripts.Helpers
{
    public static class ArrayTools
    {
        public static (int xIndex, int yIndex, int zIndex) Get3dArrayIndex(int index, int sizeX, int sizeY, int sizeZ)
        {
            var xIndex = index % (sizeZ * sizeY);
            var zIndex = index % (sizeX * sizeY);
            var yIndex = index % (sizeX * sizeZ);

            return (xIndex, zIndex, yIndex);
        }

        public static int Get1dArrayIndex(int xIndex, int yIndex, int zIndex)
        {
            return xIndex + zIndex * GameConstants.LevelCellsSizeX + yIndex * GameConstants.LevelCellsSizeZ * GameConstants.LevelCellsSizeX;
        }
    }
}