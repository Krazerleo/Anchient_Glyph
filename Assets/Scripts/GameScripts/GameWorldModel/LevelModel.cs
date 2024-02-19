using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.Interactors;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class LevelModel : IXmlSerializable
    {
        private const int CellsCount
            = GameConstants.LevelCellsSizeX * GameConstants.LevelCellsSizeY * GameConstants.LevelCellsSizeZ;

        private readonly List<CellModel> _cellModelGrid = new(CellsCount);

        public LevelModel()
        {
            var walls = new WallType[] { 0, 0, 0, 0, 0, 0 };

            for (var i = 0; i < CellsCount; i++)
            {
                _cellModelGrid.Add(new CellModel(walls));
            }
        }

        public CellModel this[int xIndex, int yIndex, int zIndex]
        {
            get => _cellModelGrid[ArrayTools.Get1dArrayIndex(xIndex, yIndex, zIndex,
                GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ)];

            set => _cellModelGrid[ArrayTools.Get1dArrayIndex(xIndex, yIndex, zIndex,
                GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ)] = value;
        }

        public CellModel this[int index]
        {
            get => _cellModelGrid[index];

            set => _cellModelGrid[index] = value;
        }

        public CellModel At(Vector3Int vec3Int)
            => this[vec3Int.x, vec3Int.y, vec3Int.z];

        public List<CellModel>.Enumerator GetEnumerator()
        {
            return _cellModelGrid.GetEnumerator();
        }

        public bool TryMoveEntity(IEntityModel entity, Vector3Int offset)
        {
            return TryMoveEntity(entity, offset.x, offset.y, offset.z);
        }

        public bool TryMoveEntity(IEntityModel entity, int xOffset, int yOffset, int zOffset)
        {
            var oldEntityPosition = entity.Position;
            var newEntityPosition = entity.Position + new Vector3Int(xOffset, yOffset, zOffset);
            
            if (CheckInBounds(newEntityPosition) == false)
            {
                return false;
            }

            if (entity.IsFullSize && At(newEntityPosition).GetEntitiesFromCell().Any(x => x.IsFullSize))
            {
                return false;
            }

            At(oldEntityPosition).RemoveEntityFromCell(entity);
            At(newEntityPosition).AddEntityToCell(entity);

            entity.Position = newEntityPosition;

            return true;
        }

        /// <summary>
        /// Get all entities which are existing at moment 
        /// of calling in level model
        /// </summary>
        /// <returns>Collection of entities</returns>
        public IEnumerable<IEntityModel> GetAllCurrentEntities()
        {
            var entities = new List<IEntityModel>();

            foreach (var cell in this)
            {
                entities.AddRange(cell.GetEntitiesFromCell());
            }

            return entities;
        }

        private bool CheckInBounds(int xPosition, int yPosition, int zPosition)
        {
            return 0 <= xPosition && 0 <= yPosition && 0 <= zPosition &&
                   xPosition < GameConstants.LevelCellsSizeX &&
                   yPosition < GameConstants.LevelCellsSizeY &&
                   zPosition < GameConstants.LevelCellsSizeZ;
        }

        private bool CheckInBounds(Vector3Int position)
        {
            return CheckInBounds(position.x, position.y, position.z);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            DeserializeLevelEnvironment(reader);
            DeserializeLevelEntities(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(XmlLevelConstants.XmlNodeLevelFileName);

            SerializeLevelEnvironment(writer);
            SerializeLevelCreatures(writer);

            writer.WriteEndElement();
        }

        private void DeserializeLevelEnvironment(XmlReader xmlReader)
        {
            var levelModelBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];

            xmlReader.ReadToDescendant(XmlLevelConstants.XmlNodeLevelEnvName);
            xmlReader.ReadElementContentAsBase64(levelModelBuffer, 0,
                CellsCount * CellModel.SizeOfElementBytes);

            for (int bytePointer = 0;
                 bytePointer < CellsCount * CellModel.SizeOfElementBytes;
                 bytePointer += CellModel.SizeOfElementBytes)
            {
                this[bytePointer / CellModel.SizeOfElementBytes]
                    = CellModel.DeserializeElement(levelModelBuffer.AsSpan(bytePointer,
                        CellModel.SizeOfElementBytes));
            }
        }

        /// <summary>
        /// Deserialize only creatures, excluding player
        /// </summary>
        /// <param name="xmlReader"></param>
        private void DeserializeLevelEntities(XmlReader xmlReader)
        {
            var xmlSerializer = new XmlSerializer(typeof(CreatureModel[]),
                new XmlRootAttribute()
                {
                    ElementName = XmlLevelConstants.XmlNodeLevelEntitiesName
                });

            xmlReader.ReadToNextSibling(XmlLevelConstants.XmlNodeLevelEntitiesName);

            var creatureModels = (CreatureModel[])xmlSerializer.Deserialize(xmlReader);
            foreach (var creatureModel in creatureModels)
            {
                At(creatureModel.Position)
                    .AddEntityToCell(creatureModel);
            }
        }

        private void SerializeLevelEnvironment(XmlWriter xmlWriter)
        {
            var arrayBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];
            var bytePointer = 0;

            foreach (var cell in this)
            {
                var cellData = cell.SerializeElement();
                var location = arrayBuffer.AsSpan().Slice(bytePointer, CellModel.SizeOfElementBytes);
                bytePointer += CellModel.SizeOfElementBytes;

                cellData.CopyTo(location);
            }

            xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelEnvName);
            xmlWriter.WriteBase64(arrayBuffer, 0, arrayBuffer.Length);
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Serialize only creatures, excluding player
        /// </summary>
        /// <param name="xmlWriter"></param>
        private void SerializeLevelCreatures(XmlWriter xmlWriter)
        {
            var xmlSerializer = new XmlSerializer(typeof(CreatureModel));

            xmlWriter.WriteStartElement(XmlLevelConstants.XmlNodeLevelEntitiesName);

            foreach (var cell in this)
            {
                foreach (var entity in cell.GetEntitiesFromCell())
                {
                    // Player Serialization in another place. It`s not available to
                    // polymorph serialize entities at least using standard serializer
                    if (entity is PlayerModel)
                    {
                        continue;
                    }

                    xmlSerializer.Serialize(xmlWriter, entity);
                }
            }

            xmlWriter.WriteEndElement();
        }
    }
}