using System;
using System.Xml;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class LevelModelSerializer
    {
        private XmlWriter _xmlWriter;

        public LevelModelSerializer(XmlWriter xmlWriter)
        {
            _xmlWriter = xmlWriter;
        }

        public void Serialize(LevelModel levelModel)
        {
            _xmlWriter.WriteStartDocument();

            _xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelFileName);

            SerializeLevelEnvironment(levelModel);

            SerializeLevelEntities(levelModel);

            _xmlWriter.WriteEndElement();

            _xmlWriter.WriteEndDocument();
        }

        private void SerializeLevelEnvironment(LevelModel levelModel)
        {
            var arrayBuffer = new byte[LevelModel.CellsCount * CellModel.SizeOfElementBytes];
            var bytePointer = 0;

            foreach (var cell in levelModel)
            {
                var cellData = cell.SerializeElement();
                var location = arrayBuffer.AsSpan().Slice(bytePointer, CellModel.SizeOfElementBytes);
                bytePointer += CellModel.SizeOfElementBytes;

                cellData.CopyTo(location);
            }

            _xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelEnvName);
            _xmlWriter.WriteBase64(arrayBuffer, 0, arrayBuffer.Length);
            _xmlWriter.WriteEndElement();
        }

        private void SerializeLevelEntities(LevelModel levelModel)
        {
            _xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelEntitiesName);

            foreach (var cell in levelModel)
            {
                var entities = cell.EntityModelsInCell;

                if (entities.IsValueCreated)
                {
                    foreach (var entity in entities.Value)
                    {
                        _xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeEntity);

                        _xmlWriter.WriteAttributeString(XmlLevelConstants.XmlPropertyEntityId, entity.Identifier);
                        _xmlWriter.WriteElementString(XmlLevelConstants.XmlPropertyEntityName, entity.Name);
                        _xmlWriter.WriteElementString(XmlLevelConstants.XmlPropertyEntityPosition, entity.Position.ToString());

                        _xmlWriter.WriteEndElement();
                    }
                }
            }

            _xmlWriter.WriteEndElement();
        }
    }
}