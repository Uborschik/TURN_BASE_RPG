using GameCore.Utils.Positions;

namespace GameCore.Grid.Interfaces
{
    public interface IGridSystem
    {
        INavigationNode[,] Nodes { get; }

        bool Contains(Position2Int position);
        INavigationNode GetCell(Position2Int position);
    }
}
