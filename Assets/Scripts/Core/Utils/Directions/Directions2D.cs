using GameCore.Utils.Positions;

namespace GameCore.Utils.Directions
{
    public static class Directions2D
    {
        public static readonly Position2Int[] CardinalDirections = new Position2Int[]
        {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0)
        };

        public static readonly Position2Int[] DiagonalDirections = new Position2Int[]
        {
            new( 1,  1),
            new( 1, -1),
            new(-1, -1),
            new( 1,  1)
        };

        public static readonly Position2Int[] EightDirections = new Position2Int[]
        {
            new( 0,  1),
            new( 1,  1),
            new( 1,  0),
            new( 1, -1),
            new( 0, -1),
            new(-1, -1),
            new(-1,  0),
            new(-1,  1)
        };
    }
}