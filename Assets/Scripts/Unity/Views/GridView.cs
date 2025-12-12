using GameCore.Grid.Interfaces;
using GameUnity.Adapters;

namespace GameUnity.Views
{
    public class GridView
    {
        private readonly TilemapAdapter renderer;

        public GridView(TilemapAdapter renderer)
        {
            this.renderer = renderer;
        }

        public void PaintAll(INavigationNode[,] grid)
        {
            foreach (var node in grid)
            {
                renderer.DrawCell(node.X, node.Y, node.IsWalkable);
            }
        }
    }
}