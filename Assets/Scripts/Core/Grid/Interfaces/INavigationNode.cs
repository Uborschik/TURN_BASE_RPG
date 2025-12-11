using System.Collections.Generic;

namespace GameCore.Grid.Interfaces
{
    public interface INavigationNode : IHeapItem<INavigationNode>
    {
        int X { get; }
        int Y { get; }

        int G { get; set; }
        int H { get; set; }
        int F { get; }
        INavigationNode Parent { get; set; }

        uint Version { get; set; }

        bool IsInClosedSet { get; set; }
        bool IsBlocked { get; set; }
        bool IsWalkable { get; }
        float MovementCost { get; set; }
        List<INavigationNode> Neighbours { get; }

        void AddNeighbors(List<INavigationNode> neighbours);

        void ResetPathData();
    }
}