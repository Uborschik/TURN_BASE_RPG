using GameCore.Models;
using GameCore.Services.Grid;
using GameCore.Services.Objects;
using GameCore.Services.Pathfinding;
using GameUnity.Adapters;
using GameUnity.Factories;
using GameUnity.Presenters;
using GameUnity.Services;
using GameUnity.Settings;
using GameUnity.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace GameUnity.Infrastructure.Composition
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [Header("Settings")]
        [SerializeField] private GridSettings gridSettings;
        [SerializeField] private GridViewSettings gridViewSettings;
        [SerializeField] private PathViewSettings pathViewSettings;
        [SerializeField] private UnitsViewSettings unitsViewSettings;

        [Header("View Components")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private SelectionTransforms selectionTransforms;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(mainCamera);
            builder.RegisterInstance(tilemap);
            builder.RegisterInstance(lineRenderer);
            builder.RegisterInstance(gridViewSettings);
            builder.RegisterInstance(pathViewSettings);
            builder.RegisterInstance(unitsViewSettings);
            builder.RegisterInstance(selectionTransforms);

            // ========== CORE: модели и базовые сервисы (без зависимостей) ==========
            builder.RegisterInstance(new GridModel(gridSettings.Width, gridSettings.Height));
            builder.Register<GridSystem>(Lifetime.Singleton);

            // ========== CORE: реестры и сервисы (зависят от GridModel) ==========
            builder.Register<GridObjectRegistry>(Lifetime.Singleton);
            builder.Register<UnitTurnOrderService>(Lifetime.Singleton);
            builder.Register<PathfindingService>(Lifetime.Singleton);

            // ========== CORE: фасады и плейсеры (зависят от реестров) ==========
            builder.Register<GridObjectPlacer>(Lifetime.Singleton);
            builder.Register<GridBlackboard>(Lifetime.Singleton);

            // ========== UNITY: адаптеры и сервисы (зависят от Unity) ==========
            builder.Register<TilemapAdapter>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton);

            // ========== UNITY: Views (зависят от адаптеров) ==========
            builder.Register<GridView>(Lifetime.Singleton);
            builder.Register<PathView>(Lifetime.Singleton);
            builder.Register<CellSelectionView>(Lifetime.Singleton);

            // ========== UNITY: Presenters (зависят от Core + Views) ==========
            builder.Register<GridPresenter>(Lifetime.Singleton);
            builder.Register<PathPresenter>(Lifetime.Singleton);
            builder.Register<UnitPresenter>(Lifetime.Singleton);
            builder.Register<CellSelectionPresenter>(Lifetime.Singleton);

            // ========== UNITY: Фабрики и регистры (зависят от Presenters) ==========
            builder.Register<UnitFactory>(Lifetime.Singleton);
            builder.Register<UnitViewRegistry>(Lifetime.Singleton);

            // ========== ENTRY POINTS (запускаются последними) ==========
            builder.RegisterEntryPoint<GridPresenter>();
            builder.RegisterEntryPoint<PathPresenter>();
            builder.RegisterEntryPoint<UnitViewRegistry>();
            builder.RegisterEntryPoint<CellSelectionPresenter>();
        }
    }
}