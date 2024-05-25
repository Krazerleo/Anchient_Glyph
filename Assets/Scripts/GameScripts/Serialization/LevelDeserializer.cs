using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;
using JetBrains.Annotations;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelDeserializer
    {
        private readonly string _levelModelPath;

        public LevelDeserializer(string levelModelPath)
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

            var model = DeserializeLevelModel(xmlReader);

            if (model is null)
            {
                throw new SerializationException("Deserialization unsuccessful: " +
                                                 "Broken level model");
            }

            var creatures = DeserializeLevelEntities(xmlReader);

            if (creatures == null)
            {
                throw new SerializationException("Deserialization unsuccessful: " +
                                                 "Broken creatures");
            }

            return model;
        }

        [CanBeNull]
        private LevelModel DeserializeLevelModel(XmlReader xmlReader)
        {
            xmlReader.ReadToDescendant(nameof(LevelModel));
            var xmlSerializer = new XmlSerializer(typeof(LevelModel));
            return xmlSerializer.Deserialize(xmlReader) as LevelModel;
        }

        [CanBeNull]
        private IEnumerable<CreatureModel> DeserializeLevelEntities(XmlReader xmlReader)
        {
            var xmlSerializer = new XmlSerializer(typeof(CreatureModel[]),
                new XmlRootAttribute()
                {
                    ElementName = XmlLevelConstants.XmlNodeLevelEntitiesName
                });

            xmlReader.ReadToFollowing(XmlLevelConstants.XmlNodeLevelEntitiesName);

            return xmlSerializer.Deserialize(xmlReader) as CreatureModel[];
        }
    }
}