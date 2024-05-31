using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelDeserializer
    {
        private readonly string _levelModelPath;

        public LevelDeserializer(string levelModelPath)
        {
            _levelModelPath = levelModelPath;
        }

        public bool TryDeserialize(out LevelModel levelModel, out ItemsSerializationContainer itemsOnScene)
        {
            try
            {
                (levelModel, itemsOnScene) = Deserialize();
                return true;
            }
            catch
            {
                levelModel = null;
                itemsOnScene = null;
                return false;
            }
        }

        public (LevelModel, ItemsSerializationContainer)  Deserialize()
        {
            using XmlReader xmlReader = XmlReader.Create(_levelModelPath);

            LevelModel levelModel = DeserializeLevelModel(xmlReader);

            if (levelModel is null)
            {
                throw new SerializationException("Deserialization unsuccessful: " +
                                                 "Broken level model");
            }

            IEnumerable<CreatureModel> creatures = DeserializeLevelEntities(xmlReader);

            if (creatures == null)
            {
                throw new SerializationException("Deserialization unsuccessful: " +
                                                 "Broken creatures");
            }

            foreach (CreatureModel creature in creatures)
            {
                levelModel[creature.Position].AddEntityToCell(creature);
            }

            ItemsSerializationContainer itemsResult = new(DeserializeItemsOnScene(xmlReader));
            
            return (levelModel, itemsResult);
        }

        [CanBeNull]
        private LevelModel DeserializeLevelModel(XmlReader xmlReader)
        {
            xmlReader.ReadToDescendant(nameof(LevelModel));
            XmlSerializer xmlSerializer = new(typeof(LevelModel));
            return xmlSerializer.Deserialize(xmlReader) as LevelModel;
        }

        [CanBeNull]
        private IEnumerable<CreatureModel> DeserializeLevelEntities(XmlReader xmlReader)
        {
            XmlSerializer xmlSerializer = new(typeof(CreatureModel[]),
                new XmlRootAttribute
                {
                    ElementName = XmlLevelConstants.EntitiesElementName
                });

            xmlReader.ReadToFollowing(XmlLevelConstants.EntitiesElementName);

            return xmlSerializer.Deserialize(xmlReader) as CreatureModel[];
        }

        private IEnumerable<ItemSerializationInfo> DeserializeItemsOnScene(XmlReader xmlReader)
        {
            List<ItemSerializationInfo> resultItems = new();
            xmlReader.ReadToFollowing(XmlLevelConstants.ItemsOnSceneElementName);
            
            while (xmlReader.ReadToFollowing(nameof(GameItem)))
            {
                string uid = xmlReader.GetAttribute(XmlLevelConstants.ItemUidAttributeName);
                Vector3 position = SerializationExtensions.ParseVector3( 
                    xmlReader.GetAttribute(XmlLevelConstants.ItemPositionAttributeName));
                
                resultItems.Add(new ItemSerializationInfo(uid, position));
            }

            return resultItems;
        }
    }
}