using GameCore.Models;
using GameCore.Utils.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Services.Objects
{
    public class UnitTurnOrderService
    {
        private readonly List<UnitModel> roundOrder = new();

        private readonly HashSet<GridObjectId> deadUnits = new();
        private readonly Dictionary<GridObjectId, UnitModel> unitCache = new();

        private int currentIndex = -1;
        private int aliveCount = 0;

        private const float DEFRAG_THRESHOLD = 0.3f;

        public bool IsEmpty => aliveCount == 0;
        public UnitModel CurrentUnit => GetCurrentUnit();

        public void RegisterUnits(IEnumerable<UnitModel> units)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));

            lock (roundOrder)
            {
                roundOrder.Clear();
                deadUnits.Clear();
                unitCache.Clear();
                currentIndex = -1;
                aliveCount = 0;

                var aliveUnits = units.Where(u => u?.IsAlive == true);
                foreach (var unit in aliveUnits)
                {
                    unitCache[unit.Id] = unit;
                }

                RebuildOrder();
            }
        }

        public void AddUnit(UnitModel unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            if (!unit.IsAlive) return;

            lock (roundOrder)
            {
                unitCache[unit.Id] = unit;

                int insertIndex = FindInsertIndex(unit);
                roundOrder.Insert(insertIndex, unit);
                aliveCount++;

                if (currentIndex >= insertIndex)
                    currentIndex++;
            }
        }

        public void RemoveUnit(GridObjectId unitId)
        {
            lock (roundOrder)
            {
                if (!unitCache.ContainsKey(unitId)) return;

                var unit = unitCache[unitId];
                unitCache.Remove(unitId);

                if (roundOrder.Contains(unit))
                {
                    deadUnits.Add(unitId);
                    aliveCount--;
                }

                var current = GetCurrentUnit();
                if (current?.Id == unitId)
                {
                    MoveNextInternal();
                }
            }
        }

        public void StartNewRound()
        {
            lock (roundOrder)
            {
                if ((float)deadUnits.Count / roundOrder.Count > DEFRAG_THRESHOLD)
                {
                    Defragment();
                }

                currentIndex = 0;
                deadUnits.Clear();

                foreach (var unit in roundOrder)
                {
                    if (unit.IsAlive)
                        unit.ResetMovementPoints();
                }
            }
        }

        public UnitModel GetCurrentUnit()
        {
            lock (roundOrder)
            {
                if (currentIndex < 0 || currentIndex >= roundOrder.Count)
                    return null;

                var unit = roundOrder[currentIndex];
                return unit.IsAlive && !deadUnits.Contains(unit.Id) ? unit : null;
            }
        }

        public void EndTurn()
        {
            lock (roundOrder)
            {
                var current = GetCurrentUnit();
                if (current == null) return;

                MoveNextInternal();
            }
        }

        private void MoveNextInternal()
        {
            if (aliveCount == 0)
            {
                currentIndex = -1;
                return;
            }

            do
            {
                currentIndex = (currentIndex + 1) % roundOrder.Count;
            }
            while (deadUnits.Contains(roundOrder[currentIndex].Id));

            if (currentIndex == 0)
            {
                StartNewRound();
            }
        }

        private int FindInsertIndex(UnitModel unit)
        {
            for (int i = 0; i < roundOrder.Count; i++)
            {
                var other = roundOrder[i];

                if (deadUnits.Contains(other.Id)) continue;
                if (unit.Initiative > other.Initiative)
                    return i;
            }
            return roundOrder.Count;
        }

        private void Defragment()
        {
            roundOrder.RemoveAll(u => !u.IsAlive || deadUnits.Contains(u.Id));
        }

        public void RebuildOrder()
        {
            var aliveUnits = unitCache.Values
                .Where(u => u.IsAlive && !deadUnits.Contains(u.Id))
                .OrderByDescending(u => u.Initiative)
                .ToList();

            roundOrder.Clear();
            roundOrder.AddRange(aliveUnits);
            aliveCount = roundOrder.Count;

            if (currentIndex >= aliveCount)
                currentIndex = aliveCount > 0 ? 0 : -1;
        }

        public IReadOnlyList<UnitModel> GetCurrentOrder() => roundOrder.AsReadOnly();
    }
}