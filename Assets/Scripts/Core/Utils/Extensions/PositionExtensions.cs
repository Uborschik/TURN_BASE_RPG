using GameCore.Utils.Positions;
using System;

namespace GameCore.Utils.Extensions
{
    public static class PositionExtensions
    {
        public static Position2Int ToFloor2Int(this Position2 position)
        {
            var x = (int)Math.Floor(position.X);
            var y = (int)Math.Floor(position.Y);

            return new Position2Int(x, y);
        }

        public static Position3Int ToFloor3Int(this Position3 position)
        {
            var x = (int)Math.Floor(position.X);
            var y = (int)Math.Floor(position.Y);
            var z = (int)Math.Floor(position.Z);

            return new Position3Int(x, y, z);
        }

        public static int ManhattanDistance(this Position2Int a, Position2Int b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static int EuclideanDistanceSquared(this Position2Int a, Position2Int b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        public static int ChebyshevDistance(this Position2Int a, Position2Int b)
        {
            return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
        }
    }
}
