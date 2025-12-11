using System;

namespace GameCore.Utils.Positions
{
    public struct Position2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Position2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is Position2 other && X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(Position2 left, Position2 right) => left.Equals(right);
        public static bool operator !=(Position2 left, Position2 right) => !left.Equals(right);
    }
}
