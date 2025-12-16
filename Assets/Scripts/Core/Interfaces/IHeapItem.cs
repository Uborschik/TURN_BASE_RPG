using System;

namespace GameCore.Interfaces
{
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}