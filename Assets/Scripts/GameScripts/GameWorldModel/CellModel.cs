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
        public const int SizeOfCellElement = 6;
        public const int SizeOfElementBytes = 6 * sizeof(uint);

        public Lazy<List<IEntityModel>> EntityModelsInCell;

        private WallType[] _cellData;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="walls">Walls array should have 6
        /// <see cref="AncientGlyph.GameScripts.Enums.Direction"/>
        /// </param>
        public CellModel(WallType[] walls)
        {
            Assert.IsTrue(walls.Length == 6);
            _cellData = new WallType[SizeOfCellElement];

            for (int i = 0; i < walls.Length; i++)
            {
                _cellData[i] = walls[i];
            }

            EntityModelsInCell = new Lazy<List<IEntityModel>>();
        }

        public Span<WallType> GetWalls => _cellData.AsSpan(0, 4);
        public bool HasCeil => _cellData[4] != 0 ? true : false;
        public bool HasFloor => _cellData[5] != 0 ? true : false;

        public void AddEntityToCell(IEntityModel entity) 
            => EntityModelsInCell.Value.Add(entity);

        /// <summary>
        /// Returns entity models from cell
        /// </summary>
        /// <param name="entityModels"></param>
        /// <returns>True if cell has entity models, otherwise false</returns>
        public bool GetEntitiesFromCell(out List<IEntityModel> entityModels)
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
            => EntityModelsInCell.Value.Remove(entity);

        public Span<byte> SerializeElement()
            => _cellData.SelectMany(wt => BitConverter.GetBytes((uint) wt)).ToArray().AsSpan();

        public void SetWall(WallType wall, Direction direction)
            => _cellData[(uint) direction] =  wall;
    }
}