using GameCore.Units.Interfaces;
using GameCore.Utils.Positions;

namespace GameCore.Units
{
    public class UnitData : IUnit
    {
        public Position2Int Position { get; set; }
        public bool IsAlive { get; set; } = true;
        public int Initiative { get; set; }
        public int MovementPoints { get; set; } = 4;
    }
}