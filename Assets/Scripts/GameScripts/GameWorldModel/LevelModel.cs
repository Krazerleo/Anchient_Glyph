using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry;
using UnityEngine;
using UnityEngine.Assertions;

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
            get => _cellModelGrid[ArrayExtensions.Get1dArrayIndex(xIndex, yIndex, zIndex,
                GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ)];

            set => _cellModelGrid[ArrayExtensions.Get1dArrayIndex(xIndex, yIndex, zIndex,
                GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ)] = value;
        }

        public CellModel this[int index]
        {
            get => _cellModelGrid[index];

            set => _cellModelGrid[index] = value;
        }

        public CellModel this[Vector3Int index]
        {
            get => this[index.x, index.y, index.z];

            set => this[index.x, index.y, index.z] = value;
        }

        public List<CellModel>.Enumerator GetEnumerator()
        {
            return _cellModelGrid.GetEnumerator();
        }

        /// <summary>
        /// Move entity if next conditions are satisfied:
        /// 1) Offset is normalized
        /// 2) Target cell is not occupied by full size entity
        /// </summary>
        /// <param name="entity">Entity to Move</param>
        /// <param name="offset">Normalized Vector Offset</param>
        /// <returns>true if successful</returns>
        public bool TryMoveEntity(IEntityModel entity, Vector3Int offset)
        {
            if (offset.magnitude <= 0.001)
            {
                return true;
            }

            Assert.IsTrue(offset.magnitude <= 1.001,
                "Offset magnitude cannot be more than 1 cell length" +
                "Divide entity movement to few offsets");

            Vector3Int oldEntityPosition = entity.Position;
            Vector3Int newEntityPosition = entity.Position + offset;

            if (CheckInBounds(newEntityPosition) == false)
            {
                return false;
            }

            if (this.CheckIsReachable(oldEntityPosition, newEntityPosition) == false)
            {
                return false;
            }

            if (entity.IsFullSize && this[newEntityPosition].GetEntitiesFromCell().Any(x => x.IsFullSize))
            {
                return false;
            }
            
            this[oldEntityPosition].RemoveEntityFromCell(entity);
            this[newEntityPosition].AddEntityToCell(entity);

            return true;
        }

        /// <summary>
        /// Get all entities which are existing at moment 
        /// of calling in level model
        /// </summary>
        /// <returns>Collection of entities</returns>
        public IEnumerable<IEntityModel> GetAllCurrentEntities()
        {
            List<IEntityModel> entities = new();

            foreach (CellModel cell in this)
            {
                entities.AddRange(cell.GetEntitiesFromCell());
            }

            return entities;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public bool CheckInBounds(int xPosition, int yPosition, int zPosition)
        {
            return 0 <= xPosition && 0 <= yPosition && 0 <= zPosition &&
                   xPosition < GameConstants.LevelCellsSizeX &&
                   yPosition < GameConstants.LevelCellsSizeY &&
                   zPosition < GameConstants.LevelCellsSizeZ;
        }

        public bool CheckInBounds(Vector3Int position)
        {
            return CheckInBounds(position.x, position.y, position.z);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader xmlReader)
        {
            byte[] levelModelBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];

            xmlReader.ReadToDescendant(XmlLevelConstants.EnvElementName);
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

        public void WriteXml(XmlWriter xmlWriter)
        {
            byte[] arrayBuffer = new byte[CellsCount * CellModel.SizeOfElementBytes];
            int bytePointer = 0;

            foreach (CellModel cell in this)
            {
                Span<byte> cellData = cell.SerializeElement();
                Span<byte> location = arrayBuffer.AsSpan().Slice(bytePointer, CellModel.SizeOfElementBytes);
                bytePointer += CellModel.SizeOfElementBytes;

                cellData.CopyTo(location);
            }

            xmlWriter.WriteStartElement(XmlLevelConstants.EnvElementName);
            xmlWriter.WriteBase64(arrayBuffer, 0, arrayBuffer.Length);
            xmlWriter.WriteEndElement();
        }
    }
}