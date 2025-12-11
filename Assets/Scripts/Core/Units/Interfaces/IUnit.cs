using GameCore.Utils.Positions;

namespace GameCore.Units.Interfaces
{
    public interface IUnit
    {
        Position2Int Position { get; set; }
        bool IsAlive { get; }
        int Initiative { get; }
        int MovementPoints { get; }
    }
}