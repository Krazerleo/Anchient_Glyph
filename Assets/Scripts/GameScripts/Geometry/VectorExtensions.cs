using UnityEngine;

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
    }
}