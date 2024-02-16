using System;
using System.Xml;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelModelDeserializer
    {
        private string _levelModelPath = string.Empty;

        public LevelModelDeserializer(string levelModelPath)
        {
            _levelModelPath = levelModelPath;
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
            using var xmlReader = XmlReader.Create(_levelModelPath);
            var xmlSerializer = new XmlSerializer(typeof(LevelModel));
            var model = xmlSerializer.Deserialize(xmlReader) as LevelModel;

            if (model is null)
            {
                throw new ArgumentException("Deserialization unsuccessful");
            }

            return model;
        }
    }
}