namespace AncientGlyph.GameScripts.Geometry
{
    public static class ArrayExtensions
    {
        public static (int xIndex, int yIndex, int zIndex)
            Get3dArrayIndex(int index, int sizeX, int sizeZ)
        {
            var yIndex = index / (sizeX * sizeZ);
            index -= yIndex * sizeX * sizeZ;

            var xIndex = index % sizeX;
            var zIndex = index / sizeX;

            return (xIndex, yIndex, zIndex);
        }

        public static int Get1dArrayIndex(int xIndex, int yIndex, int zIndex,
                        int sizeX, int sizeZ)
        {
            return xIndex + (zIndex * sizeX) + (yIndex * sizeX * sizeZ);
        }
    }
}