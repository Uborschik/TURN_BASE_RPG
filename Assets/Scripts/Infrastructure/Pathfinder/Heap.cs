using GameCore.Grid.Interfaces;
using System;
using System.Collections.Generic;

namespace GameInfrastructure.Pathfinder
{
    public class Heap<T> where T : IHeapItem<T>
    {
        private T[] items;
        private int currentItemCount;
        private static readonly EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        private const int MINIMUM_CAPACITY = 4;
        internal const int MAXIMUM_CAPACITY = 8192;

        public int Count => currentItemCount;
        public int Capacity => items.Length;

        public Heap(int initialCapacity)
        {
            if (initialCapacity < MINIMUM_CAPACITY) initialCapacity = MINIMUM_CAPACITY;
            items = new T[initialCapacity];
        }

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            GrowIfNeeded();

            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            currentItemCount++;
            SortUp(item);
        }

        public T RemoveFirst()
        {
            if (currentItemCount == 0)
                throw new InvalidOperationException("Heap is empty");

            var firstItem = items[0];
            firstItem.HeapIndex = -1;

            currentItemCount--;

            if (currentItemCount > 0)
            {
                items[0] = items[currentItemCount];
                items[0].HeapIndex = 0;
                SortDown(items[0]);
            }

            return firstItem;
        }

        public void UpdateItem(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (item.HeapIndex < 0 || item.HeapIndex >= currentItemCount)
                throw new InvalidOperationException("Item is not in the heap");

            SortUp(item);
            SortDown(item);
        }

        public bool Contains(T item)
        {
            if (item == null) return false;

            return item.HeapIndex >= 0 &&
                   item.HeapIndex < currentItemCount &&
                   comparer.Equals(items[item.HeapIndex], item);
        }

        public void Clear()
        {
            for (int i = 0; i < currentItemCount; i++)
            {
                if (items[i] != null)
                    items[i].HeapIndex = -1;
            }
            currentItemCount = 0;
        }

        private void GrowIfNeeded()
        {
            if (currentItemCount < items.Length) return;

            int newSize = Math.Min(items.Length * 2, MAXIMUM_CAPACITY);
            if (newSize <= items.Length) return;

            var newItems = new T[newSize];
            Array.Copy(items, newItems, currentItemCount);
            items = newItems;
        }

        private void SortDown(T item)
        {
            int parentIndex = item.HeapIndex;

            while (parentIndex < currentItemCount)
            {
                int leftChildIndex = parentIndex * 2 + 1;
                int rightChildIndex = parentIndex * 2 + 2;
                int smallestChildIndex = leftChildIndex;

                if (leftChildIndex >= currentItemCount) break;

                if (rightChildIndex < currentItemCount &&
                    items[rightChildIndex].CompareTo(items[leftChildIndex]) < 0)
                {
                    smallestChildIndex = rightChildIndex;
                }

                if (items[smallestChildIndex].CompareTo(item) < 0)
                {
                    Swap(item, items[smallestChildIndex]);
                    parentIndex = item.HeapIndex;
                }
                else break;
            }
        }

        private void SortUp(T item)
        {
            int childIndex = item.HeapIndex;

            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                var parentItem = items[parentIndex];

                if (item.CompareTo(parentItem) < 0)
                {
                    Swap(item, parentItem);
                    childIndex = item.HeapIndex;
                }
                else break;
            }
        }

        private void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            (itemB.HeapIndex, itemA.HeapIndex) = (itemA.HeapIndex, itemB.HeapIndex);
        }
    }
}