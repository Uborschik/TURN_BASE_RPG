using GameCore.Grid.Interfaces;
using GameInfrastructure.Pathfinder.Interfaces;
using GameUnity.Infrastructure.Interfaces;
using GameUnity.Utils.Extensions;
using UnityEngine;

namespace GameUnity.Views
{
    public class GlobalView
    {
        private readonly IGridSystem grid;
        private readonly IPathfinder pathfinder;
        private readonly GridView gridView;
        private readonly PathView pathView;
        private readonly CellSelectionView cellSelectionView;

        private Vector2? rawStart;
        private Vector2? rawEnd;

        public GlobalView(IServiceLocator serviceLocator, GridView gridView, PathView pathView, CellSelectionView cellSelectionView)
        {
            grid = serviceLocator.Get<IGridSystem>();
            pathfinder = serviceLocator.Get<IPathfinder>();
            this.gridView = gridView;
            this.pathView = pathView;
            this.cellSelectionView = cellSelectionView;
        }

        public void OnStart()
        {
            gridView.PaintAll(grid.Nodes);
        }

        public void OnClick(Vector2 value)
        {
            cellSelectionView.Select(value.ToCenter3D());

            if (!rawStart.HasValue) rawStart = value;
            else rawEnd = value;

            if (rawStart.HasValue && rawEnd.HasValue)
            {
                DrawPath(rawStart.Value, rawEnd.Value);
                rawStart = rawEnd;
            }
        }

        public void ResetPoints()
        {
            rawStart = null;
            rawEnd = null;
            pathView.ClearPath();
            cellSelectionView.ClearSelectTile();
        }

        public void HighlightCell(Vector2 value)
        {
            cellSelectionView.Highlight(value.ToCenter3D());
        }

        private void DrawPath(Vector2 sv, Vector2 ev)
        {
            var start = sv.ToFloorPosition2Int();
            var end = ev.ToFloorPosition2Int();

            if (!grid.Contains(start) || !grid.Contains(end)) return;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var path = pathfinder.FindPath(start, end);
            sw.Stop();
            pathView.DrawPath(path);
            Debug.Log($"Path found with {path.Count} nodes in {sw.Elapsed.TotalMilliseconds:F2}ms");
        }
    }
}
