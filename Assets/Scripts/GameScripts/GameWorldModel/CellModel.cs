using System;
using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Interactors.Interfaces;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    /// <summary>
    /// Represents 3D world in discrete format by integer cell.
    /// Reserve entities and other functional objects.
    /// </summary>
    public struct CellModel
    {
        #region Public Fields

        public const int SizeOfCellElement = 6;
        public const int SizeOfElementBytes = 6 * sizeof(uint);

        public Lazy<ICollection<IEntityModel>> EntityModelsInCell;

        #endregion Public Fields

        #region Private Fields

        private uint[] _cellData;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="walls">Walls array should have 6
        /// <see cref="AncientGlyph.GameScripts.Enums.Direction"/></param>
        /// <param name="hasFloor">has floor</param>
        public CellModel(WallType[] walls)
        {
            Assert.IsTrue(walls.Length == 6);
            _cellData = new uint[SizeOfCellElement];

            for (int i = 0; i < walls.Length; i++)
            {
                _cellData[i] = (uint) walls[i];
            }

            EntityModelsInCell = new Lazy<ICollection<IEntityModel>>();
        }

        #endregion Public Constructors

        #region Public Properties

        public Span<uint> GetWalls => _cellData.AsSpan(1, 4);

        public bool HasCeil => _cellData[5] == 1 ? true : false;

        public bool HasFloor => _cellData[4] == 1 ? true : false;

        #endregion Public Properties

        #region Public Methods

        public void AddEntityToCell(IEntityModel entity)
        {
            EntityModelsInCell.Value.Add(entity);
        }

        public CellModel DeserializeElement(Span<byte> bytes)
        {
            var walls = new WallType[6];

            for (var i = 0; i < bytes.Length; i += sizeof(uint))
            {
                walls[i/ sizeof(uint)] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls);
        }

        /// <summary>
        /// Returns entity models from cell
        /// </summary>
        /// <param name="entityModels"></param>
        /// <returns>True if cell has entity models, otherwise false</returns>
        public bool GetEntitiesFromCell(out ICollection<IEntityModel> entityModels)
        {
            if (EntityModelsInCell.IsValueCreated)
            {
                entityModels = null;
                return false;
            }

            entityModels = EntityModelsInCell.Value;

            return true;
        }

        public void RemoveEntityFromCell(IEntityModel entity)
        {
            EntityModelsInCell.Value.Remove(entity);
        }

        public Span<byte> SerializeElement()
        {
            return _cellData.SelectMany(BitConverter.GetBytes).ToArray().AsSpan();
        }

        public void SetWall(WallType wall, Direction direction)
        {
            _cellData[(uint) direction] = (uint) wall;
        }

        #endregion Public Methods
    }
}