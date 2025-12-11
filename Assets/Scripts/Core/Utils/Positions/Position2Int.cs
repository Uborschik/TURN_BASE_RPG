using System;

namespace GameCore.Utils.Positions
{
    public struct Position2Int
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is Position2Int other && X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(Position2Int left, Position2Int right) => left.Equals(right);
        public static bool operator !=(Position2Int left, Position2Int right) => !left.Equals(right);
    }
}
