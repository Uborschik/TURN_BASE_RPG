using GameCore.Units;
using GameCore.Utils.Positions;
using System.Collections.Generic;

namespace GameCore.Grid
{
    public class GridBlackboard
    {
        private readonly GridSystem grid;
        private readonly Dictionary<Position2Int, UnitData> allUnits = new();

        public GridBlackboard(GridSystem grid)
        {
            this.grid = grid;
        }

        public void PlaceUnit(UnitData unit, Position2Int position)
        {
            if (!grid.Contains(position)) return;

            if (grid.Contains(unit.Position))
            {
                var oldCell = grid.GetCell(unit.Position);
                if (oldCell != null) oldCell.IsBlocked = false;
            }

            var newCell = grid.GetCell(position) as GridCell;
            if (newCell != null && newCell.MovementCost > 0)
            {
                newCell.IsBlocked = true;
                unit.Position = position;
                allUnits[position] = unit;
            }
        }

        public void RemoveUnit(UnitData unit)
        {
            if (grid.Contains(unit.Position))
            {
                var cell = grid.GetCell(unit.Position);
                if (cell != null) cell.IsBlocked = false;
            }
            allUnits.Remove(unit.Position);
        }

        public UnitData GetUnitAt(Position2Int position) => allUnits.TryGetValue(position, out var unit) ? unit : null;

        public bool IsCellFree(Position2Int position) => GetUnitAt(position) == null;
    }
}