using GameCore.Models;
using GameCore.Utils.Positions;

namespace GameCore.Interfaces
{
    public interface IGridObject
    {
        uint Id { get; }
        Position2Int? Position { get; }
        bool BlocksMovement { get; }
        Team Team { get; }
        bool IsStatic { get; }

        void SetPosition(Position2Int position);
    }
}