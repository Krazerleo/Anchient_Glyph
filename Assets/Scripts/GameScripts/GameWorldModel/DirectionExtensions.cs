using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public static class DirectionExtensions
    {
        public static Direction GetDirectionFromNormalizedOffset(this Vector3Int offset)
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

        public static Vector3Int GetNormalizedOffsetFromDirection(this Direction direction)
        {
            return direction switch
            {
                Direction.East => new Vector3Int(1, 0, 0),
                Direction.North => new Vector3Int(0, 0, 1),
                Direction.West => new Vector3Int(-1, 0, 0),
                Direction.South => new Vector3Int(0, 0, -1),
                Direction.Up => new Vector3Int(0, 1, 0),
                Direction.Down => new Vector3Int(0, -1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}