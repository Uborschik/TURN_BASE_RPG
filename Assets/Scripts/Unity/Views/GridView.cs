using GameCore.Interfaces;
using GameCore.Utils.Positions;
using GameUnity.Adapters;
using UnityEngine;

namespace GameUnity.Views
{
    public class GridView
    {
        private readonly TilemapAdapter renderer;

        public GridView(TilemapAdapter renderer)
        {
            this.renderer = renderer;
        }

        public Vector2 GridToWorld(Position2Int position)
        {
            return new Vector2(position.X + 0.5f, position.Y + 0.5f);
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