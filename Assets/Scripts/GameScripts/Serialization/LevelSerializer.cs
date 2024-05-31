using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelSerializer
    {
        private readonly string _levelModelPath;

        public LevelSerializer(string levelModelPath)
        {
            _levelModelPath = levelModelPath;
        }

        public void Serialize(LevelModel levelModel, IEnumerable<(string, Vector3)> itemsOnScene)
        {
            using XmlWriter xmlWriter = XmlWriter.Create(_levelModelPath, new XmlWriterSettings()
            {
                Indent = true,
            });
            xmlWriter.WriteStartElement(XmlLevelConstants.FileElementName);

            SerializeLevelModel(xmlWriter, levelModel);
            SerializeLevelCreatures(xmlWriter, levelModel.GetAllCurrentEntities());
            SerializeItemsOnScene(xmlWriter, itemsOnScene);

            xmlWriter.WriteEndElement();
        }

        private void SerializeLevelModel(XmlWriter xmlWriter, LevelModel levelModel)
        {
            XmlSerializer xmlSerializer = new(typeof(LevelModel));
            xmlSerializer.Serialize(xmlWriter, levelModel);
        }

        private void SerializeLevelCreatures(XmlWriter xmlWriter, IEnumerable<IEntityModel> creatureEntities)
        {
            XmlSerializer xmlSerializer = new(typeof(CreatureModel));

            xmlWriter.WriteStartElement(XmlLevelConstants.EntitiesElementName);

            foreach (IEntityModel creatureEntity in creatureEntities)
            {
                if (creatureEntity is CreatureModel creature)
                {
                    xmlSerializer.Serialize(xmlWriter, creature);
                }
            }

            xmlWriter.WriteEndElement();
        }

        private void SerializeItemsOnScene(XmlWriter xmlWriter, IEnumerable<(string, Vector3)> itemsOnScene)
        {
            xmlWriter.WriteStartElement(XmlLevelConstants.ItemsOnSceneElementName);

            foreach ((string itemUid, Vector3 pos) gameItem in itemsOnScene)
            {
                xmlWriter.WriteStartElement(nameof(GameItem));
                xmlWriter.WriteAttributeString(XmlLevelConstants.ItemUidAttributeName, gameItem.itemUid);
                xmlWriter.WriteAttributeString(XmlLevelConstants.ItemPositionAttributeName, gameItem.pos.ToString());
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }
    }
}