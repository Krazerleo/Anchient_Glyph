using System.Xml;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelModelSerializer
    {
        private string _levelModelPath = string.Empty;

        public LevelModelSerializer(string levelModelPath)
        {
            _levelModelPath = levelModelPath;
        }

        public bool TrySerialize(LevelModel levelModel)
        {
            try
            {
                Serialize(levelModel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Serialize(LevelModel levelModel)
        {
            using var xmlWriter = XmlWriter.Create(_levelModelPath, new XmlWriterSettings()
            {
                Indent = true
            });
            var xmlSerializer = new XmlSerializer(typeof(LevelModel));

            xmlSerializer.Serialize(xmlWriter, levelModel);
        }
    }
}