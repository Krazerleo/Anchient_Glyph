using System;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public static class CellModelExtensions
    {
        /// <summary>
        /// Check if is other endCell is reachable
        /// from startCell. Maximum distance
        /// is only 1 else UB.
        /// </summary>
        /// <returns></returns>
        /// <remarks>CellModel has not any coordinates, so its client responsibility to
        /// calculate offset between start cell and end cell :(</remarks>
        public static bool CheckIsReachable(this CellModel startCell, CellModel endCell, Vector3Int offset)
        {
            if (offset.sqrMagnitude == 0)
            {
                return true;
            }

            if (offset.sqrMagnitude != 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(offset)} is greater than 1");
            }

            var direction = offset.GetDirectionFromNormalizedOffset();

            switch (direction)
            {
                case Direction.East:
                case Direction.West:
                    return startCell.GetWalls[(int)Direction.East] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.West] != WallType.Whole;
                case Direction.North:
                case Direction.South:
                    return startCell.GetWalls[(int)Direction.North] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.South] != WallType.Whole;
                case Direction.Up:
                case Direction.Down:
                    return startCell.GetWalls[(int)Direction.Up] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.Down] != WallType.Whole;
                default:
                    throw new ArgumentOutOfRangeException($"{direction} - Unexpected type of Direction Enum");
            }
        }
    }
}