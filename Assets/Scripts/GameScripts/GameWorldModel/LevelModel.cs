using System.Collections;
using System.Collections.Generic;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class LevelModel : IEnumerable<CellModel>
    {
        public const int LevelCellsSizeX = 32;
        public const int LevelCellsSizeZ = 32;
        public const int LevelCellsSizeY = 8;

        public const int CellsCount = LevelCellsSizeZ * LevelCellsSizeY * LevelCellsSizeX;

        private CellModel[,,] _cellModelGrid;

        public LevelModel()
        {
            _cellModelGrid = new CellModel[LevelCellsSizeX, LevelCellsSizeZ, LevelCellsSizeY];
        }

        public CellModel this[int xIndex, int zIndex, int yIndex]
        {
            get => _cellModelGrid[xIndex, zIndex, yIndex];

            set => _cellModelGrid[xIndex, zIndex, yIndex] = value;
        }

        public IEnumerator<CellModel> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => _cellModelGrid.GetEnumerator();
    }
}