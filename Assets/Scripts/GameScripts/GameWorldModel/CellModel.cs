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
        public const int SizeOfCellElement = 5;
        public const int SizeOfElementBytes = 5 * sizeof(uint);

        private uint[] _cellData;

        public Lazy<ICollection<IEntityModel>> EntityModelsInCell;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="walls">Walls array should have 4 elements
        /// of north, east, south and west walls</param>
        /// <param name="hasFloor"></param>
        public CellModel(WallType[] walls, bool hasFloor)
        {
            Assert.IsTrue(walls.Length == 4);
            _cellData = new uint[SizeOfCellElement];
            _cellData[0] = hasFloor ? 1u : 0u;

            for (int i = 0; i < walls.Length; i++)
            {
                _cellData[i + 1] = (uint) walls[i];
            }

            EntityModelsInCell = new Lazy<ICollection<IEntityModel>>();
        }

        public void AddEntityToCell(IEntityModel entity)
        {
            EntityModelsInCell.Value.Add(entity);
        }

        public void RemoveEntityFromCell(IEntityModel entity)
        {
            EntityModelsInCell.Value.Remove(entity);
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

        public void SetWall(WallType wall, Direction direction)
        {
            _cellData[(uint) direction] = (uint) wall;
        }

        public Span<uint> GetWalls => _cellData.AsSpan(1, 4);

        public bool HasFloor => _cellData[0] == 1 ? true : false;

        public Span<byte> SerializeElement()
        {
            var uintArray = new uint[SizeOfCellElement];

            uintArray[0] = HasFloor ? 1u : 0u;

            for (var i = 1; i < 5; i++)
            {
                uintArray[i] = _cellData[i];
            }

            return uintArray.SelectMany(BitConverter.GetBytes).ToArray().AsSpan();
        }

        public CellModel DeserializeElement(Span<byte> bytes)
        {
            var walls = new WallType[4];
            bool hasFloor = BitConverter.ToUInt32(bytes[0..4]) == 1 ? true : false;

            for (var i = 1; i < bytes.Length; i += 4)
            {
                walls[i] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls, hasFloor);
        }
    }
}