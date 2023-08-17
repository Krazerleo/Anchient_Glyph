namespace AncientGlyph.GameScripts.Helpers
{
    public static class ArrayTools
    {
        public static (int xIndex, int zIndex, int yIndex) Get3dArrayIndex(int index, int sizeX, int sizeZ, int sizeY)
        {
            var xIndex = index % (sizeZ * sizeY);
            var zIndex = index % (sizeX * sizeY);
            var yIndex = index % (sizeX * sizeZ);

            return (xIndex, zIndex, yIndex);
        }
    }
}