using GameCore.Models;
using GameCore.Services.Objects;
using GameCore.Utils.Directions;
using GameCore.Utils.Positions;
using GameCore.Utils.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Services.Grid
{
    public class GridObjectPlacer
    {
        private readonly GridSystem system;
        private readonly GridObjectRegistry objects;

        public GridObjectPlacer(GridSystem system, GridObjectRegistry objects)
        {
            this.system = system;
            this.objects = objects;
        }

        public void InitializePositions(List<UnitModel> units)
        {
            var players = units.Where(u => u.Team == Team.Player).ToList();
            var enemies = units.Where(u => u.Team == Team.Enemy).ToList();

            PlaceUnits(players, true);
            PlaceUnits(enemies, false);
        }

        private void PlaceUnits(List<UnitModel> units, bool isPlayer)
        {
            var x = isPlayer ? 1 : system.Model.Width - 2;
            var y = Dice.RollNumBetween(1, system.Model.Height - 2);
            var center = new Position2Int(x, y);

            for (int i = 0; i < units.Count; i++)
            {
                var unit = units[i];
                var direction = Directions2D.CardinalDirections[i];

                objects.PlaceObject(unit, center + direction);
            }
        }
    }
}