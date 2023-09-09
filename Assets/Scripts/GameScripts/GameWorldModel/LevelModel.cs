using System.Collections;
using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class LevelModel : IEnumerable<CellModel>
    {
        public const int CellsCount = GameConstants.LevelCellsSizeX * GameConstants.LevelCellsSizeY * GameConstants.LevelCellsSizeZ;

        private readonly CellModel[,,] _cellModelGrid;

        public LevelModel()
        {
            _cellModelGrid = new CellModel[GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeY, GameConstants.LevelCellsSizeZ];
        }

        public CellModel this[int xIndex, int yIndex, int zIndex]
        {
            get => _cellModelGrid[xIndex, yIndex, zIndex];

            set => _cellModelGrid[xIndex, yIndex, zIndex] = value;
        }

        public IEnumerator<CellModel> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveEntity(IEntityModel entity, int xOffset, int yOffset, int zOffset)
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
                _cellModelGrid[entityPosition.x, entityPosition.y, entityPosition.z]
                    .EntityModelsInCell.Value.Remove(entity);

                _cellModelGrid[entityPosition.x + xOffset, entityPosition.y + yOffset, entityPosition.z + zOffset]
                    .EntityModelsInCell.Value.Add(entity);

                return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int x = 0; x < GameConstants.LevelCellsSizeX; x++)
            {
                for (int y = 0; y < GameConstants.LevelCellsSizeY; y++)
                {
                    for (int z = 0; z < GameConstants.LevelCellsSizeZ; z++)
                    {
                        yield return _cellModelGrid[x, y, z];
                    }
                }
            }
        }
    }
}