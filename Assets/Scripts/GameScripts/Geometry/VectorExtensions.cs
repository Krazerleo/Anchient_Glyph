using System;
using AncientGlyph.GameScripts.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.Geometry
{
    public static class VectorExtensions
    {
        public static Vector3Int ToVector3Int(this Vector3 vector, bool toFloor = true)
        {
            return toFloor
                ? new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z))
                : new Vector3Int(Mathf.CeilToInt(vector.x), Mathf.CeilToInt(vector.y), Mathf.CeilToInt(vector.z));
        }
        
        public static Vector3Int SetXInt(this Vector3Int vector, int xCoordinate)
        {
            return new Vector3Int(xCoordinate, vector.y, vector.z);
        }

        public static Vector3Int SetYInt(this Vector3Int vector, int yCoordinate)
        {
            return new Vector3Int(vector.x, yCoordinate, vector.z);
        }

        public static Vector3Int SetZInt(this Vector3Int vector, int zCoordinate)
        {
            return new Vector3Int(vector.x, vector.y, zCoordinate);
        }
        
        public static Direction GetDirectionFromNormalizedOffset(Vector3Int offset)
        {
            Assert.IsTrue(Math.Abs(offset.magnitude - 1) < 0.001,
                "Offset magnitude cannot be more than 1 cell length");
            
            switch (offset.x)
            {
                case 1:
                    return Direction.East;
                case -1:
                    return Direction.West;
            }
            
            switch (offset.z)
            {
                case 1:
                    return Direction.North;
                case -1:
                    return Direction.South;
            }
            
            switch (offset.y)
            {
                case 1:
                    return Direction.Up;
                case -1:
                    return Direction.Down;
            }
            
            Debug.LogError("Unreachable code detected");
            return 0;
        }
    }
}