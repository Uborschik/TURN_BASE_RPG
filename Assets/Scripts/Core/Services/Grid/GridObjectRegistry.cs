using GameCore.Interfaces;
using GameCore.Models;
using GameCore.Utils;
using GameCore.Utils.Positions;
using System;
using System.Collections.Generic;

namespace GameCore.Services.Grid
{
    public class GridObjectRegistry
    {
        private readonly GridSystem system;
        private readonly Dictionary<GridObjectId, IGridObject> objects = new();
        private readonly Dictionary<Position2Int, GridObjectId> positionIndex = new();
        private readonly Dictionary<ObjectType, List<IGridObject>> byType = new();
        private readonly Dictionary<Team, List<IGridObject>> byTeam = new();

        public event Action<IGridObject> ObjectAdded;
        public event Action<IGridObject> ObjectRemoved;

        public GridObjectRegistry(GridSystem system)
        {
            this.system = system;

            foreach (ObjectType type in Enum.GetValues(typeof(ObjectType)))
                byType[type] = new List<IGridObject>();
            foreach (Team team in Enum.GetValues(typeof(Team)))
                byTeam[team] = new List<IGridObject>();
        }

        public Result PlaceObject(IGridObject obj, Position2Int position)
        {
            if (!system.Contains(position))
                return Result.Fail("Invalid position");

            if (positionIndex.ContainsKey(position) && obj.BlocksMovement)
                return Result.Fail("Cell blocked");

            var cell = system.GetCell(position);
            if (cell == null || cell.MovementCost <= 0)
                return Result.Fail("Cell not walkable");

            var id = new GridObjectId(obj.Id, DetectType(obj));
            obj.SetPosition(position);

            objects[id] = obj;
            positionIndex[position] = id;
            byType[id.Type].Add(obj);
            byTeam[obj.Team].Add(obj);

            cell.IsBlocked |= obj.BlocksMovement;
            ObjectAdded?.Invoke(obj);

            return Result.Ok();
        }

        public void RemoveObject(GridObjectId id)
        {
            if (!objects.TryGetValue(id, out var obj)) return;

            if (obj.Position is { } pos)
            {
                positionIndex.Remove(pos);
                if (system.Contains(pos))
                    system.GetCell(pos).IsBlocked = false;
            }

            objects.Remove(id);
            byType[id.Type].Remove(obj);
            byTeam[obj.Team].Remove(obj);

            ObjectRemoved?.Invoke(obj);
        }

        public IGridObject GetObjectAt(Position2Int position)
        {
            return positionIndex.TryGetValue(position, out var id) ? objects[id] : null;
        }

        public IReadOnlyList<IGridObject> GetByType(ObjectType type)
            => byType[type].AsReadOnly();

        public IReadOnlyList<IGridObject> GetByTeam(Team team)
            => byTeam[team].AsReadOnly();

        private ObjectType DetectType(IGridObject obj) => obj switch
        {
            UnitModel => ObjectType.Unit,
            _ => ObjectType.Decoration
        };
    }
}