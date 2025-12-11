using System;

namespace GameCore.Utils.Positions
{
    public struct Position3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Position3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj) => obj is Position3 other && X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        public static bool operator ==(Position3 left, Position3 right) => left.Equals(right);
        public static bool operator !=(Position3 left, Position3 right) => !left.Equals(right);
    }
}