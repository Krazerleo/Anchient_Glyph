using System;
using System.Xml;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelModelDeserializer
    {
        private XmlReader _xmlReader;

        public LevelModelDeserializer(XmlReader xmlReader)
        {
            _xmlReader = xmlReader;
        }

        public bool TryDeserialize(out LevelModel levelModel)
        {
            try
            {
                levelModel = Deserialize();
                return true;
            }
            catch
            {
                levelModel = null;
                return false;
            }
        }

        public LevelModel Deserialize()
        {
            var levelModel = new LevelModel();

            DeserializeLevelEnvironment(levelModel);

            DeserializeLevelEntities(levelModel);

            return levelModel;
        }

        private void DeserializeLevelEnvironment(LevelModel levelModel)
        {
            var levelModelBuffer = new byte[LevelModel.CellsCount * CellModel.SizeOfElementBytes];

            _xmlReader.ReadToDescendant(XmlLevelConstants.XmlNodeLevelEnvName);
            _xmlReader.ReadElementContentAsBase64(levelModelBuffer, 0,
                                                  LevelModel.CellsCount * CellModel.SizeOfElementBytes);

            for (int bytePointer = 0;
                bytePointer < LevelModel.CellsCount * CellModel.SizeOfElementBytes;
                bytePointer += CellModel.SizeOfElementBytes)
            {
                levelModel[bytePointer / CellModel.SizeOfElementBytes]
                    = CellModelDeserializer.Deserialize(levelModelBuffer.AsSpan(bytePointer,
                                                                CellModel.SizeOfElementBytes));
            }
        }

        private void DeserializeLevelEntities(LevelModel levelModel)
        {
            _xmlReader.ReadToFollowing(XmlLevelConstants.XmlNodeLevelEntitiesName);

        }
    }
}