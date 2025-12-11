using GameCore.Units.Interfaces;
using GameCore.Utils.Positions;

namespace GameCore.Grid.Interfaces
{
    public interface IGridOccupancy
    {
        void PlaceUnit(IUnit unit, Position2Int position);
        void RemoveUnit(IUnit unit);
        IUnit GetUnitAt(Position2Int position);
        bool IsCellFree(Position2Int position);
    }
}