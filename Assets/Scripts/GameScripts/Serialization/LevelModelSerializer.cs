using System;

using AncientGlyph.GameScripts.Helpers;
using AncientGlyph.GameScripts.ModelElements;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public class LevelModelSerializer : IModelDataSerializer<LevelModel>
    {
        private const uint ElementSize = CellModel.SizeOfElementBytes;

        public LevelModel DeserializeElement(Span<byte> bytes)
        {
            var levelModel = new LevelModel();
            int gridIterator = 0;

            var cellSerializer = new CellModelSerializer();

            for (uint bytesIterator = 0; bytesIterator < bytes.Length; bytesIterator += ElementSize)
            {
                var multiIndex = ArrayTools.Get3dArrayIndex(gridIterator, LevelModel.LevelCellsSizeX, LevelModel.LevelCellsSizeZ, LevelModel.LevelCellsSizeY);
                levelModel.CellModelGrid[multiIndex.xIndex, multiIndex.zIndex, multiIndex.yIndex] = cellSerializer.DeserializeElement(bytes.Slice((int) bytesIterator, (int) ElementSize));
            }

            return levelModel;
        }

        public Span<byte> SerializeElement(LevelModel element)
        {
            throw new NotImplementedException();
        }
    }
}