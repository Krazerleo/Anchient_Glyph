using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Helpers;
using AncientGlyph.GameScripts.Interactors.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    [XmlRoot("LevelModel")]
    public class LevelModel : IXmlSerializable
    {
        #region Public Fields

        public const int CellsCount = GameConstants.LevelCellsSizeX * GameConstants.LevelCellsSizeY * GameConstants.LevelCellsSizeZ;

        #endregion Public Fields

        #region Private Fields

        private List<CellModel> _cellModelGrid;

        #endregion Private Fields

        #region Public Constructors

        public LevelModel()
        {
            _cellModelGrid = new List<CellModel>
            {
                Capacity = CellsCount
            };

            var walls = new WallType[6] { 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < CellsCount; i++)
            {
                _cellModelGrid.Add(new CellModel(walls));
            }
        }

        #endregion Public Constructors

        #region Public Indexers

        public CellModel this[int xIndex, int yIndex, int zIndex]
        {
            get => _cellModelGrid[ArrayTools.Get1dArrayIndex(xIndex, yIndex, zIndex)];

            set => _cellModelGrid[ArrayTools.Get1dArrayIndex(xIndex, yIndex, zIndex)] = value;
        }

        #endregion Public Indexers

        #region Public Methods

        public IEnumerator<CellModel> GetEnumerator()
        {
            return _cellModelGrid.GetEnumerator();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public bool MoveEntity(IEntityModel entity, int xOffset, int yOffset, int zOffset)
        {
            var entityPosition = entity.Position;

            //Check if entity can be accomodated in next cell.
            if (entity.IsFullSize && this[entityPosition.x + xOffset, entityPosition.y + yOffset, entityPosition.z + zOffset]
                .EntityModelsInCell.Value.Any(x => x.IsFullSize))
            {
                return false;
            }
            else
            {
                this[entityPosition.x, entityPosition.y, entityPosition.z]
                    .EntityModelsInCell.Value.Remove(entity);

                this[entityPosition.x + xOffset, entityPosition.y + yOffset, entityPosition.z + zOffset]
                    .EntityModelsInCell.Value.Add(entity);

                return true;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            // Deserialize level walls, floors, ceil and other in binary format
            var levelModelBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];
            reader.ReadToDescendant(FileConstants.XmlNodeLevelModelName);
            reader.ReadElementContentAsBase64(levelModelBuffer, 0, CellsCount * CellModel.SizeOfElementBytes);

            for (int bytePointer = 0; bytePointer < CellsCount * CellModel.SizeOfElementBytes; bytePointer += CellModel.SizeOfElementBytes)
            {
                _cellModelGrid[bytePointer / CellModel.SizeOfElementBytes].DeserializeElement(levelModelBuffer.AsSpan(bytePointer, CellModel.SizeOfElementBytes));
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            // Serialize level walls, floors, ceil and other in binary format
            var arrayBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];
            var bytePointer = 0;

            foreach (var cell in this)
            {
                var cellData = cell.SerializeElement();
                var location = arrayBuffer.AsSpan().Slice(bytePointer, bytePointer + CellModel.SizeOfElementBytes);

                cellData.CopyTo(location);
            }

            if (arrayBuffer.Select(b => b != 0b00000000).Any())
            {
                Debug.Log("findes");
            }

            writer.WriteStartElement(FileConstants.XmlNodeLevelModelName);
            writer.WriteBase64(arrayBuffer, 0, arrayBuffer.Length);
            writer.WriteEndElement();

            // TODO : Make Serialization for entities
            foreach (var cell in this)
            {
                var entities = cell.EntityModelsInCell;
                if (entities.IsValueCreated)
                {
                    foreach (var entity in entities.Value)
                    {
                        entity.WriteXml(writer);
                    }
                }
            }
        }

        #endregion Public Methods
    }
}