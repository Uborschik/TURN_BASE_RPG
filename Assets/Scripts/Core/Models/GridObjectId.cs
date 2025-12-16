using System;

namespace GameCore.Models
{
    public enum ObjectType
    {
        Unit,
        Obstacle,
        Item,
        Trigger,
        Decoration
    }

    public readonly struct GridObjectId : IEquatable<GridObjectId>
    {
        public readonly uint Value;
        public readonly ObjectType Type;

        public GridObjectId(uint value, ObjectType type)
        {
            Value = value;
            Type = type;
        }

        public bool Equals(GridObjectId other) => Value == other.Value && Type == other.Type;
        public override bool Equals(object obj) => obj is GridObjectId other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Value, (int)Type);
        public static bool operator ==(GridObjectId left, GridObjectId right) => left.Equals(right);
        public static bool operator !=(GridObjectId left, GridObjectId right) => !left.Equals(right);
    }
}