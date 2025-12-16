using GameCore.Models;
using GameCore.Services.Grid;
using GameCore.Utils.Extensions;
using GameCore.Utils.Positions;
using System;
using System.Collections.Generic;

namespace GameCore.Services.Pathfinding
{
    public class PathfindingService
    {
        private readonly GridSystem system;
        private readonly AStar pathfinder;

        public PathfindingService(GridSystem system)
        {
            this.system = system ?? throw new ArgumentNullException(nameof(system));
            pathfinder = new AStar(system.Model.Nodes);
        }

        public List<Position2Int> FindPath(Position2Int start, Position2Int end)
        {
            var startNode = system.GetCell(start);
            var endNode = system.GetCell(end);

            return (startNode == null || endNode == null)
                ? new List<Position2Int>()
                : pathfinder.FindPath(startNode, endNode);
        }

        public bool HasLineOfSight(Position2Int from, Position2Int to, int maxRange)
        {
            if (from.ChebyshevDistance(to) > maxRange)
                return false;

            var endCell = system.GetCell(to);
            if (endCell == null || !endCell.IsWalkable)
                return false;

            return BresenhamLineOfSight(from, to, maxRange);
        }

        private bool BresenhamLineOfSight(Position2Int from, Position2Int to, int maxRange)
        {
            var current = from;
            int dx = Math.Abs(to.X - from.X);
            int dy = Math.Abs(to.Y - from.Y);
            int sx = from.X < to.X ? 1 : -1;
            int sy = from.Y < to.Y ? 1 : -1;
            int err = dx - dy;

            int maxSteps = Math.Max(dx, dy) * 2;
            int steps = 0;

            while (steps <= maxRange && steps++ < maxSteps)
            {
                if (current == to) return true;

                var cell = system.GetCell(current);
                if (cell != null && !cell.IsWalkable) return false;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    current.X += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    current.Y += sy;
                }
            }

            return steps <= maxRange;
        }
    }
}