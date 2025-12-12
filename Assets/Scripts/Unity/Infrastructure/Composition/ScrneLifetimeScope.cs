using GameCore.Grid;
using GameInfrastructure.Pathfinder;
using GameUnity.Adapters;
using GameUnity.Infrastructure.Services;
using GameUnity.Settings;
using GameUnity.Views;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameUnity.Infrastructure.Composition
{
    public class ScrneLifetimeScope : LifetimeScope
    {

        [Header("Settings")]
        [SerializeField] private GridSettings gridSettings;
        [SerializeField] private GridViewSettings gridViewSettings;
        [SerializeField] private PathViewSettings pathViewSettings;

        [Header("View Components")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform highlightTile;
        [SerializeField] private Transform selectTile;

        protected override void Configure(IContainerBuilder builder)
        {
            var gridConfig = new GridConfig(gridSettings.Width, gridSettings.Height);
            var selectionTransforms = new SelectionTransforms(highlightTile, selectTile);

            builder.RegisterInstance(mainCamera);
            builder.RegisterInstance(tilemap);
            builder.RegisterInstance(lineRenderer);
            builder.RegisterInstance(gridViewSettings);
            builder.RegisterInstance(pathViewSettings);
            builder.RegisterInstance(gridConfig);
            builder.RegisterInstance(selectionTransforms);

            builder.Register<InputService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GridSystem>(Lifetime.Singleton);
            builder.Register<AStarPathfinder>(Lifetime.Singleton);
            builder.Register<TilemapAdapter>(Lifetime.Singleton);
            builder.Register<GridView>(Lifetime.Singleton);
            builder.Register<PathView>(Lifetime.Singleton);
            builder.Register<CellSelectionView>(Lifetime.Singleton);
            builder.Register<GlobalView>(Lifetime.Singleton);

            builder.RegisterBuildCallback(resolver => resolver.Resolve<GlobalView>().OnStart());

            builder.RegisterBuildCallback(SetupInput);
        }

        private void SetupInput(IObjectResolver resolver)
        {
            var globalView = resolver.Resolve<GlobalView>();
            var inputs = resolver.Resolve<InputService>();

            inputs.LeftClick += () =>
                globalView.OnClick(GetMouseWorldPosition());

            inputs.RightClick += () =>
                globalView.ResetPoints();
        }

        private Vector2 GetMouseWorldPosition()
        {
            return mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}