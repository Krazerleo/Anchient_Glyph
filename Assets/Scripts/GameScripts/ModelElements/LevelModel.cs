namespace AncientGlyph.GameScripts.ModelElements
{
    public class LevelModel
    {
        public const int LevelCellsSizeX = 32;
        public const int LevelCellsSizeZ = 32;
        public const int LevelCellsSizeY = 8;

        public CellModel[,,] CellModelGrid;

        public LevelModel()
        {
            CellModelGrid = new CellModel[LevelCellsSizeX, LevelCellsSizeZ, LevelCellsSizeY];
        }
    }
}