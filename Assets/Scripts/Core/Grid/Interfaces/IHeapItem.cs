using System;

namespace GameCore.Grid.Interfaces
{
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}