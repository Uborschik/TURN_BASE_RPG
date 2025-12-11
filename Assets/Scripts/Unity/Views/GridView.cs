using GameCore.Grid.Interfaces;
using GameUnity.Adapters.Interfaces;

namespace GameUnity.Views
{
    public class GridView
    {
        private readonly IGridRenderer renderer;

        public GridView(IGridRenderer renderer)
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