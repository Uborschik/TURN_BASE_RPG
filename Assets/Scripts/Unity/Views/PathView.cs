using GameCore.Utils.Positions;
using GameUnity.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnity.Views
{
    public class PathView
    {
        private readonly LineRenderer linePrefab;

        public PathView(LineRenderer linePrefab, PathViewSettings settings)
        {
            this.linePrefab = linePrefab;

            linePrefab.startColor = settings.PathColor;
            linePrefab.endColor = settings.PathColor;
            linePrefab.startWidth = settings.PathWidth;
            linePrefab.endWidth = settings.PathWidth;
        }

        public void DrawPath(List<Position2Int> pathPoints)
        {
            if (pathPoints == null || pathPoints.Count < 2)
            {
                HidePath();
                return;
            }

            linePrefab.positionCount = pathPoints.Count;

            for (int i = 0; i < pathPoints.Count; i++)
            {
                Vector3 point = new Vector3(pathPoints[i].X + 0.5f, pathPoints[i].Y + 0.5f);
                linePrefab.SetPosition(i, point);
            }

            linePrefab.enabled = true;
        }

        private void HidePath()
        {
            linePrefab.enabled = false;
        }

        public void ClearPath()
        {
            linePrefab.positionCount = 0;
            HidePath();
        }
    }
}