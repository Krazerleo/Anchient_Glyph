using System;
using System.IO;
using AncientGlyph.GameScripts.Helpers;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public class LevelModelSerializer
    {
        public LevelModel DeserializeElement(BinaryReader reader)
        {
            var levelModel = new LevelModel();

            var cellSerializer = new CellModelSerializer();

            for (int gridIterator = 0; gridIterator < LevelModel.CellsCount; gridIterator++)
            {
                var multiIndex = ArrayTools.Get3dArrayIndex(gridIterator, LevelModel.LevelCellsSizeX, LevelModel.LevelCellsSizeZ, LevelModel.LevelCellsSizeY);
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
    }
}