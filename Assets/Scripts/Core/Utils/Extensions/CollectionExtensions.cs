using GameCore.Utils.Positions;
using GameCore.Utils.Random;
using System.Collections.Generic;

namespace GameCore.Utils.Extensions
{
    public static class CollectionExtensions
    {
        public static bool TryGetRandom<T>(this IList<T> list, out T value)
        {
            if (list == null || list.Count == 0)
            {
                value = default;
                return false;
            }
            value = list[Dice.RollIndex(list.Count)];
            return true;
        }

        public static bool TryGetRandom<T>(this T[] array, out T value)
        {
            if (array == null || array.Length == 0)
            {
                value = default;
                return false;
            }
            value = array[Dice.RollIndex(array.Length)];
            return true;
        }

        public static bool Contains<T>(this T[,] array, int x, int y)
        {
            return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
        }

        public static bool Contains<T>(this T[,] array, Position2Int position)
        {
            var x = position.X;
            var y = position.Y;

            return Contains(array, x, y);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Dice.RollIndexBetween(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}