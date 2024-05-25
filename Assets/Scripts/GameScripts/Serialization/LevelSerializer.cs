using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelSerializer
    {
        private readonly string _levelModelPath;

        public LevelSerializer(string levelModelPath)
        {
            _levelModelPath = levelModelPath;
        }

        public void Serialize(LevelModel levelModel)
        {
            using var xmlWriter = XmlWriter.Create(_levelModelPath, new XmlWriterSettings()
            {
                Indent = true,
            });
            xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelFileName);
            
            SerializeLevelModel(xmlWriter, levelModel);
            SerializeLevelCreatures(xmlWriter, levelModel.GetAllCurrentEntities());

            xmlWriter.WriteEndElement();
        }

        private void SerializeLevelModel(XmlWriter xmlWriter, LevelModel levelModel)
        {
            var xmlSerializer = new XmlSerializer(typeof(LevelModel));
            xmlSerializer.Serialize(xmlWriter, levelModel);
        }
        
        private void SerializeLevelCreatures(XmlWriter xmlWriter, IEnumerable<IEntityModel> creatureEntities)
        {
            var xmlSerializer = new XmlSerializer(typeof(CreatureModel));

            xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelEntitiesName);

            foreach (var creatureEntity in creatureEntities)
            {
                if (creatureEntity is CreatureModel creature)
                {
                    xmlSerializer.Serialize(xmlWriter, creature);
                }
            }

            xmlWriter.WriteEndElement();
        }
    }
}