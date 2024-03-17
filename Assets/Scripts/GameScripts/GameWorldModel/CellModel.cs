using System;
using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.Enums;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    /// <summary>
    /// Represents 3D world in discrete format by integer cell.
    /// Reserve entities and other functional objects.
    /// </summary>
    public readonly struct CellModel
    {
        private const int SizeOfCellElement = 6;
        public const int SizeOfElementBytes = SizeOfCellElement * sizeof(uint);

        private readonly List<IEntityModel> _entityModelsInCell;

        private readonly WallType[] _cellData;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="walls">Walls array should have 6
        /// <see cref="Direction"/>
        /// </param>
        public CellModel(WallType[] walls)
        {
            Assert.IsTrue(walls.Length == 6);
            _cellData = new WallType[SizeOfCellElement];

            for (int i = 0; i < walls.Length; i++)
            {
                _cellData[i] = walls[i];
            }

            _entityModelsInCell = new List<IEntityModel>();
        }

        public Span<WallType> GetWalls => _cellData.AsSpan(0, 6);
        public bool HasCeil => _cellData[4] != 0;
        public bool HasFloor => _cellData[5] != 0;

        public void AddEntityToCell(IEntityModel entity)
            => _entityModelsInCell.Add(entity);

        public void RemoveEntityFromCell(IEntityModel entity)
        {
            _entityModelsInCell.Remove(entity);
        }

        public IEnumerable<IEntityModel> GetEntitiesFromCell()
        {
            return _entityModelsInCell;
        }

        public Span<byte> SerializeElement()
            => _cellData.SelectMany(wt => BitConverter.GetBytes((uint) wt)).ToArray().AsSpan();

        public static CellModel DeserializeElement(Span<byte> bytes)
        {
            var walls = new WallType[6];

            for (var i = 0; i < bytes.Length; i += sizeof(uint))
            {
                walls[i / sizeof(uint)] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls);
        }

        public void SetWall(WallType wall, Direction direction)
            => _cellData[(uint) direction] =  wall;
    }
}