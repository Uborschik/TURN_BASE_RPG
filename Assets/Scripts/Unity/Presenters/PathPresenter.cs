using GameCore.Interfaces;
using GameCore.Services.Pathfinding;
using GameUnity.Services;
using GameUnity.Utils.Extensions;
using GameUnity.Views;
using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using VContainer.Unity;

namespace GameUnity.Presenters
{
    public class PathPresenter : IInitializable, IDisposable
    {
        private readonly InputService inputs;
        private readonly PathfindingService pathfinder;
        private readonly PathView pathView;
        private Vector2? startPoint;
        private Vector2? endPoint;

        public PathPresenter(InputService inputs, PathfindingService pathfinder, PathView pathView)
        {
            this.inputs = inputs;
            this.pathfinder = pathfinder;
            this.pathView = pathView;

        }

        public void Initialize()
        {
            inputs.LeftClick += HandleClick;
            inputs.RightClick += Reset;
        }

        public void Dispose()
        {
            inputs.LeftClick -= HandleClick;
            inputs.RightClick -= Reset;
        }

        private void HandleClick()
        {
            var position = inputs.GetScreenPosition();

            if (!startPoint.HasValue) startPoint = position;
            else endPoint = position;

            if (startPoint.HasValue && endPoint.HasValue)
            {
                var start = startPoint.Value.ToFloorPosition2Int();
                var end = endPoint.Value.ToFloorPosition2Int();
                var path = pathfinder.FindPath(start, end);
                pathView.DrawPath(path);
                startPoint = endPoint;
            }
        }

        private void Reset()
        {
            startPoint = null;
            pathView.ClearPath();
        }
    }
}