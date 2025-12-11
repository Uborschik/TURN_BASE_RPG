using GameCore.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameCore.Utils.Random
{
    public static class Dice
    {
        [ThreadStatic] private static System.Random random;

        private static System.Random Instance
        {
            get
            {
                if (random == null)
                {
                    int seed = Environment.TickCount ^ Thread.CurrentThread.ManagedThreadId;
                    random = new System.Random(seed);
                }
                return random;
            }
        }

        public static int GetValueAt(params int[] diceFaces)
        {
            if (diceFaces == null || diceFaces.Length == 0)
                return 0;

            var sumValue = 0;
            for (int i = 0; i < diceFaces.Length; i++)
            {
                if (diceFaces[i] > 0)
                    sumValue += RollNum(diceFaces[i]);
            }
            return sumValue;
        }

        public static T RollFrom<T>(this T[] values)
        {
            if (values.TryGetRandom(out var value))
                return value;
            throw new ArgumentException("Values array cannot be null or empty", nameof(values));
        }

        public static T RollFrom<T>(this IList<T> values)
        {
            if (values.TryGetRandom(out var value))
                return value;
            throw new ArgumentException("Values list cannot be null or empty", nameof(values));
        }

        public static int RollNumBetween(int min, int max)
        {
            if (min > max) (min, max) = (max, min);
            return Instance.Next(min, max + 1);
        }

        public static int RollIndexBetween(int min, int max)
        {
            if (min >= max) return min;
            return Instance.Next(min, max);
        }

        public static int RollNum(int diceFaces)
        {
            if (diceFaces <= 1) return 1;
            return Instance.Next(1, diceFaces + 1);
        }

        public static int RollIndex(int collectionSize)
        {
            if (collectionSize <= 0) return 0;
            return Instance.Next(0, collectionSize);
        }

        public static bool RollChance(int percent)
        {
            if (percent <= 0) return false;
            if (percent >= 100) return true;
            return RollNumBetween(1, 100) <= percent;
        }

        public static bool RollBool() => Instance.Next(2) == 0;
    }
}