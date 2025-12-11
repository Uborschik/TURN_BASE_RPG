using GameCore.Grid.Interfaces;

namespace GameCore.Grid.Settings
{
    public class GridConfig
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }
        public IMovementCostProvider MovementCostProvider { get; }

        public GridConfig(int width, int height, float cellSize, IMovementCostProvider movementCostProvider = null)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            MovementCostProvider = movementCostProvider ?? new DefaultMovementCostProvider();
        }
    }
}