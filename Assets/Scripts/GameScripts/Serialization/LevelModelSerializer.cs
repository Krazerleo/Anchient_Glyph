using System.IO;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Helpers;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public class LevelModelSerializer
    {
        #region Public Methods

        public LevelModel DeserializeElement(BinaryReader reader)
        {
            var levelModel = new LevelModel();

            var cellSerializer = new CellModelSerializer();

            for (int gridIterator = 0; gridIterator < LevelModel.CellsCount; gridIterator++)
            {
                var multiIndex = ArrayTools.Get3dArrayIndex(gridIterator, GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeY, GameConstants.LevelCellsSizeZ);
                levelModel[multiIndex.xIndex, multiIndex.yIndex, multiIndex.zIndex] = cellSerializer.DeserializeElement(reader);
            }

            return levelModel;
        }

        public void SerializeElement(LevelModel level, BinaryWriter writer)
        {
            var cellSerializer = new CellModelSerializer();

            foreach (var cell in level)
            {
                cellSerializer.SerializeElement(cell, writer);
            }
        }

        #endregion Public Methods
    }
}