using System;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.Geometry
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
    }
}