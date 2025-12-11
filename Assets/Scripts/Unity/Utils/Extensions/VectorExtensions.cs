using GameCore.Utils.Positions;
using UnityEngine;

namespace GameUnity.Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2Int ToFloor2DInt(this Vector3 v)
        {
            return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector2Int ToFloor2DInt(this Vector2 v)
        {
            return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector3Int ToFloor3DInt(this Vector3 v)
        {
            return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
        }


        public static Position2 ToPosition2(this Vector2 v)
        {
            return new Position2(v.x, v.y);
        }

        public static Position2Int ToFloorPosition2Int(this Vector2 v)
        {
            return new Position2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector2Int To2DInt(this Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        public static Vector3Int To3DInt(this Vector2Int v)
        {
            return new Vector3Int(v.x, v.y, 0);
        }

        public static Vector2 ToCenter2D(this Vector3 v)
        {
            var intPos = v.ToFloor3DInt();
            return new Vector2(intPos.x + 0.5f, intPos.y + 0.5f);
        }

        public static Vector3 ToCenter3D(this Vector2 v)
        {
            var intPos = v.ToFloor3DInt();
            return new Vector3(intPos.x + 0.5f, intPos.y + 0.5f);
        }

        public static Vector3Int ToFloor3DInt(this Vector2 v)
        {
            var v3 = (Vector3)v;
            return new Vector3Int(Mathf.FloorToInt(v3.x), Mathf.FloorToInt(v3.y), Mathf.FloorToInt(v3.z));
        }
    }
}