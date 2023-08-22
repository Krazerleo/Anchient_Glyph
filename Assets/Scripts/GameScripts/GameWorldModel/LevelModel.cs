using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Interactors.Creatures.Controllers.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class LevelModel : IEnumerable<CellModel>
    {
        public const int LevelCellsSizeX = 32;
        public const int LevelCellsSizeZ = 32;
        public const int LevelCellsSizeY = 8;

        public const int CellsCount = LevelCellsSizeZ * LevelCellsSizeY * LevelCellsSizeX;

        private readonly CellModel[,,] _cellModelGrid;

        public LevelModel()
        {
            _cellModelGrid = new CellModel[LevelCellsSizeX, LevelCellsSizeZ, LevelCellsSizeY];
        }

        public CellModel this[int xIndex, int zIndex, int yIndex]
        {
            get => _cellModelGrid[xIndex, zIndex, yIndex];

            set => _cellModelGrid[xIndex, zIndex, yIndex] = value;
        }

        public IEnumerator<CellModel> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveEntity(IEntityModel entity, int xOffset, int zOffset, int yOffset)
        {
            var entityPosition = entity.Position;

            //Check if entity can be accomodated in next cell.
            if (entity.IsFullSize && _cellModelGrid[entityPosition.x + xOffset, entityPosition.y + yOffset, entityPosition.z + zOffset]
                .EntityModelsInCell.Value.Any(x => x.IsFullSize))
            {
                return false;
            }
            else
            {
                _cellModelGrid[entityPosition.x, entityPosition.y, entityPosition.z].EntityModelsInCell.Value.Remove(entity);
                _cellModelGrid[entityPosition.x + xOffset, entityPosition.y + yOffset, entityPosition.z + zOffset].EntityModelsInCell.Value.Add(entity);
                return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int x = 0; x < LevelCellsSizeX; x++)
            {
                for (int z = 0; z < LevelCellsSizeZ; z++)
                {
                    for (int y = 0; y < LevelCellsSizeY; y++)
                    {
                        yield return _cellModelGrid[x, z, y];
                    }
                }
            }
        }
    }
}