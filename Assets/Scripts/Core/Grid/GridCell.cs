using GameCore.Grid.Interfaces;
using System.Collections.Generic;

namespace GameCore.Grid
{
    public class GridCell : INavigationNode
    {
        private readonly int x;
        public int X => x;

        private readonly int y;
        public int Y => y;

        public int G { get; set; }
        public int H { get; set; }
        public int F => G + H;
        public int HeapIndex { get; set; } = -1;
        public uint Version { get; set; }
        public float MovementCost { get; set; }
        public bool IsBlocked { get; set; } = false;
        public bool IsWalkable => MovementCost > 0 && !IsBlocked;
        public bool IsInClosedSet { get; set; }
        public INavigationNode Parent { get; set; }
        public List<INavigationNode> Neighbours { get; private set; } = new();

        public GridCell(int x, int y, float movementCost = 1f)
        {
            this.x = x;
            this.y = y;
            MovementCost = movementCost;

            ResetPathData();
        }

        public void ResetPathData()
        {
            G = int.MaxValue;
            H = 0;
            Parent = null;
            HeapIndex = -1;
            IsInClosedSet = false;
            Version = 0;
        }

        public int CompareTo(INavigationNode other)
        {
            if (other == null) return -1;

            var compare = F.CompareTo(other.F);
            if (compare == 0)
            {
                compare = H.CompareTo(other.H);
            }
            return compare;
        }

        public void AddNeighbors(List<INavigationNode> neighbours) => Neighbours = neighbours;
    }
}