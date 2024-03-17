using System;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures
{
    public static class CellRayCaster
    {
        /// <summary>
        /// Checks if the ray crosses the cells` walls.
        /// The ray is subject to the condition that it
        /// has zero z and x components else UB.
        /// </summary>
        /// <returns>True if ray collided with wall else false</returns>
        public static bool IsRayCollided(Vector3Int startPosition, Vector3Int targetPosition, LevelModel levelModel)
        {
            // Ray is going on X Axis
            if (startPosition.z == targetPosition.z)
            {
                var currentPosition = startPosition;
                var frontPosition = startPosition;
                frontPosition.x += 1;

                for (int i = startPosition.x; i < targetPosition.x - 1; i++)
                {
                    if (levelModel.CheckIsReachable(currentPosition, frontPosition) == false)
                    {
                        return false;
                    }

                    currentPosition.x += 1;
                    frontPosition.x += 1;
                }

                return true;
            }
            
            // Ray is going on Z Axis
            if (startPosition.x == targetPosition.x)
            {
                var currentPosition = startPosition;
                var frontPosition = startPosition;
                frontPosition.z += 1;

                for (int i = startPosition.z; i < targetPosition.z - 1; i++)
                {
                    if (levelModel.CheckIsReachable(currentPosition, frontPosition) == false)
                    {
                        return false;
                    }

                    currentPosition.z += 1;
                    frontPosition.z += 1;
                }

                return true;
            }

            throw new ArgumentException("Start and target position must be X or Z aligned");
        }
    }
}