using System;

namespace GameCore.Utils.Positions
{
    public struct Position3Int
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj) => obj is Position3Int other && X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        public static bool operator ==(Position3Int left, Position3Int right) => left.Equals(right);
        public static bool operator !=(Position3Int left, Position3Int right) => !left.Equals(right);
    }
}