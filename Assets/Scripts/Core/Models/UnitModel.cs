using GameCore.Interfaces;
using GameCore.Utils.Positions;

namespace GameCore.Models
{
    public enum Team
    {
        Player,
        Enemy,
        Neutral,
        Ally
    }

    public class UnitModel : IGridObject
    {
        public GridObjectId Id { get; }
        public Team Team { get; }
        public Position2Int? Position { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public int Initiative { get; }
        public int MovementPoints { get; private set; }

        uint IGridObject.Id => Id.Value;
        bool IGridObject.BlocksMovement => true;
        bool IGridObject.IsStatic => false;
        Team IGridObject.Team => Team.Neutral;

        public UnitModel(GridObjectId id, int initiative, Team team)
        {
            Id = id;
            Initiative = initiative;
            Team = team;
            ResetMovementPoints();
        }

        public void SetPosition(Position2Int position) => Position = position;
        public void TakeDamage() => IsAlive = false;
        public void ResetMovementPoints() => MovementPoints = 4;
    }
}