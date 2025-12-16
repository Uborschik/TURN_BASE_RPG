using GameCore.Interfaces;

namespace GameCore.Models
{
    public class GridModel
    {
        public int Width { get; }
        public int Height { get; }
        public INavigationNode[,] Nodes { get; }

        public GridModel(int width, int height)
        {
            Width = width;
            Height = height;
            Nodes = new GridCell[width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    Nodes[x, y] = new GridCell(x, y);
        }
    }
}