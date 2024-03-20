using System;
using AncientGlyph.GameScripts.Enums;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public static class LevelModelExtensions
    {
        /// <summary>
        /// Check if other End Cell is reachable
        /// from neighbour Start Cell.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if cells are not neighbours</exception>
        public static bool CheckIsReachable(this LevelModel levelModel,
            Vector3Int startPosition, Vector3Int endPosition)
        {
            var offset = endPosition - startPosition;
            var startCell = levelModel.At(startPosition);
            var endCell = levelModel.At(endPosition);
            
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
                    return startCell.GetWalls[(int)Direction.East] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.West] != WallType.Whole;
                case Direction.West:
                    return startCell.GetWalls[(int)Direction.West] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.East] != WallType.Whole;
                case Direction.North:
                    return startCell.GetWalls[(int)Direction.North] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.South] != WallType.Whole;
                case Direction.South:
                    return startCell.GetWalls[(int)Direction.South] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.North] != WallType.Whole;
                case Direction.Up:
                    return startCell.GetWalls[(int)Direction.Up] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.Down] != WallType.Whole;
                case Direction.Down:
                    return startCell.GetWalls[(int)Direction.Down] != WallType.Whole &&
                           endCell.GetWalls[(int)Direction.Up] != WallType.Whole;
                default:
                    throw new ArgumentOutOfRangeException($"{direction} - Unexpected type of Direction Enum");
            }
        }
        
        /// <summary>
        /// Check if a ray intersects walls
        /// from startPosition to targetPosition
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if positions are not X or Z aligned</exception>
        public static bool IsRayCollided(this LevelModel levelModel, Vector3Int startPosition, Vector3Int targetPosition)
        {
            // Ray is going on X Axis
            if (startPosition.z == targetPosition.z)
            {
                if (startPosition.x > targetPosition.x)
                {
                    (startPosition, targetPosition) = (targetPosition, startPosition);
                }
                
                var currentPosition = startPosition;
                var frontPosition = startPosition;
                frontPosition.x += 1;

                for (int i = startPosition.x; i < targetPosition.x; i++)
                {
                    if (levelModel.CheckIsReachable(currentPosition, frontPosition) == false)
                    {
                        return true;
                    }

                    currentPosition.x += 1;
                    frontPosition.x += 1;
                }

                return false;
            }
            
            // Ray is going on Z Axis
            if (startPosition.x == targetPosition.x)
            {
                if (startPosition.z > targetPosition.z)
                {
                    (startPosition, targetPosition) = (targetPosition, startPosition);
                }
                
                var currentPosition = startPosition;
                var frontPosition = startPosition;
                frontPosition.z += 1;

                for (int i = startPosition.z; i < targetPosition.z; i++)
                {
                    if (levelModel.CheckIsReachable(currentPosition, frontPosition) == false)
                    {
                        return true;
                    }

                    currentPosition.z += 1;
                    frontPosition.z += 1;
                }

                return false;
            }

            throw new ArgumentException("Start and target position must be X or Z aligned");
        }
    }
}