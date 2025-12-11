using GameCore.Utils.Positions;
using System.Collections.Generic;

namespace GameInfrastructure.Pathfinder.Interfaces
{
    public interface IPathfinder
    {
        List<Position2Int> FindPath(Position2Int start, Position2Int end);
        bool HasLineOfSight(Position2Int from, Position2Int to, int maxRange);
    }
}
