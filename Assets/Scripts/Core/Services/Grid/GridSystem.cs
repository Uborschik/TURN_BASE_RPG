using GameCore.Interfaces;
using GameCore.Models;
using GameCore.Utils.Directions;
using GameCore.Utils.Extensions;
using GameCore.Utils.Positions;
using System.Collections.Generic;

namespace GameCore.Services.Grid
{
    public class GridSystem
    {
        private readonly GridModel model;

        public GridModel Model => model;
        public int Width => model.Width;
        public int Height => model.Height;

        public GridSystem(GridModel model)
        {
            this.model = model;
            AddNeighbors();
        }

        public bool Contains(Position2Int position)
        {
            return model.Nodes.Contains(position);
        }

        public INavigationNode GetCell(Position2Int position)
        {
            if (!Contains(position)) return null;

            return model.Nodes[position.X, position.Y];
        }

        private void AddNeighbors()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var neighbours = new List<INavigationNode>(8);

                    foreach (var dir in Directions2D.EightDirections)
                    {
                        var nx = x + dir.X;
                        var ny = y + dir.Y;

                        if (!model.Nodes.Contains(nx, ny)) continue;

                        if (dir.X != 0 && dir.Y != 0)
                        {
                            var wH = model.Nodes[x + dir.X, y].IsWalkable;
                            var wV = model.Nodes[x, y + dir.Y].IsWalkable;

                            if (!wH || !wV)
                                continue;
                        }
                        neighbours.Add(model.Nodes[nx, ny]);
                    }

                    model.Nodes[x, y].AddNeighbors(neighbours);
                }
            }
        }
    }
}