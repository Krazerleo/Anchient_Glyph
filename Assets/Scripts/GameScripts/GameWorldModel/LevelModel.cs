using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Helpers;
using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class LevelModel
    {
        public const int CellsCount
            = GameConstants.LevelCellsSizeX * GameConstants.LevelCellsSizeY * GameConstants.LevelCellsSizeZ;

        private readonly List<CellModel> _cellModelGrid = new(CellsCount);

        public LevelModel()
        {
            var walls = new WallType[6] { 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < CellsCount; i++)
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

        public IEnumerator<CellModel> GetEnumerator()
        {
            return _cellModelGrid.GetEnumerator();
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
    }
}