using System;
using System.Globalization;
using UnityEngine;

namespace AncientGlyph.GameScripts.Serialization.Extensions
{
    public static class SerializationExtensions
    {
        public static Vector3Int ParseVector3Int(string stringedVec3Int)
        {
            var middlePart = stringedVec3Int[1..^1];

            try
            {
                var splitted = middlePart.Split(',');

                if (splitted.Length != 3)
                {
                    throw new Exception();
                }

                return new Vector3Int(int.Parse(splitted[0]),
                                      int.Parse(splitted[1]),
                                      int.Parse(splitted[2]));
            }
            catch
            {
                throw new ArgumentException("Cannot deserialize Vector3Int from string"); 
            }
        }

        public static Vector3 ParseVector3(string stringedVec3)
        {
            var middlePart = stringedVec3[1..^1];

            try
            {
                var splitted = middlePart.Split(',');

                if (splitted.Length != 3)
                {
                    throw new Exception();
                }

                return new Vector3(float.Parse(splitted[0], CultureInfo.InvariantCulture.NumberFormat),
                                   float.Parse(splitted[1], CultureInfo.InvariantCulture.NumberFormat),
                                   float.Parse(splitted[2], CultureInfo.InvariantCulture.NumberFormat));
            }
            catch
            {
                throw new ArgumentException("Cannot deserialize Vector3 from string");
            }
        }
    }
}