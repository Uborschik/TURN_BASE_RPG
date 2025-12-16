using GameCore.Models;
using GameUnity.Adapters;
using GameUnity.Views;
using UnityEngine.Tilemaps;
using VContainer.Unity;

namespace GameUnity.Presenters
{
    public class GridPresenter : IInitializable
    {
        private readonly GridModel grid;
        private readonly GridView gridView;

        public GridPresenter(GridModel grid, GridView gridView)
        {
            this.grid = grid;
            this.gridView = gridView;
        }

        public void Initialize()
        {
            gridView.PaintAll(grid.Nodes);
        }
    }
}