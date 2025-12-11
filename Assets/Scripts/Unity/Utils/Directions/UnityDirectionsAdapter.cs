using GameCore.Utils.Directions;
using GameCore.Utils.Positions;
using UnityEngine;

namespace GameUnity.Utils.Directions
{
    public static class UnityDirectionsAdapter
    {
        private static readonly Vector3Int[] cardinalDirections;
        private static readonly Vector3Int[] diagonalDirections;
        private static readonly Vector3Int[] eightDirections;

        static UnityDirectionsAdapter()
        {
            cardinalDirections = ConvertToVector3Int(Directions2D.CardinalDirections);
            diagonalDirections = ConvertToVector3Int(Directions2D.DiagonalDirections);
            eightDirections = ConvertToVector3Int(Directions2D.EightDirections);
        }

        public static Vector3Int[] CardinalDirections => cardinalDirections;

        public static Vector3Int[] DiagonalDirections => diagonalDirections;

        public static Vector3Int[] EightDirections => eightDirections;

        private static Vector3Int[] ConvertToVector3Int(Position2Int[] corePositions)
        {
            if (corePositions == null) return new Vector3Int[0];

            var unityArray = new Vector3Int[corePositions.Length];
            for (int i = 0; i < corePositions.Length; i++)
            {
                unityArray[i] = new Vector3Int(corePositions[i].X, corePositions[i].Y, 0);
            }
            return unityArray;
        }

        public static Vector3Int[] ToVector3Int(this Position2Int[] positions)
        {
            return ConvertToVector3Int(positions);
        }
    }
}