using GameCore.Grid.Interfaces;
using GameCore.Utils.Directions;
using GameCore.Utils.Extensions;
using GameCore.Utils.Positions;
using System.Collections.Generic;

namespace GameCore.Grid
{
    public class GridConfig
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }

        public GridConfig(int width, int height, float cellSize = 1f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
        }
    }

    public class GridSystem
    {
        public readonly GridConfig Config;
        public INavigationNode[,] Nodes { get; }

        public GridSystem(GridConfig config)
        {
            this.Config = config;

            Nodes = new GridCell[config.Width, config.Height];

            for (int y = 0; y < config.Height; y++)
                for (int x = 0; x < config.Width; x++)
                    Nodes[x, y] = new GridCell(x, y, 1);

            AddNeighbors();
        }

        public bool Contains(Position2Int position)
        {
            return Nodes.Contains(position);
        }

        public INavigationNode GetCell(Position2Int position)
        {
            if (!Contains(position)) return null;

            return Nodes[position.X, position.Y];
        }

        private void AddNeighbors()
        {
            var w = Nodes.GetLength(0);
            var h = Nodes.GetLength(1);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var neighbours = new List<INavigationNode>(8);

                    foreach (var dir in Directions2D.EightDirections)
                    {
                        var nx = x + dir.X;
                        var ny = y + dir.Y;

                        if (nx < 0 || nx >= w || ny < 0 || ny >= h) continue;

                        if (dir.X != 0 && dir.Y != 0)
                        {
                            var wH = Nodes[x + dir.X, y].IsWalkable;
                            var wV = Nodes[x, y + dir.Y].IsWalkable;

                            if (!wH || !wV)
                                continue;
                        }
                        neighbours.Add(Nodes[nx, ny]);
                    }

                    Nodes[x, y].AddNeighbors(neighbours);
                }
            }
        }
    }
}