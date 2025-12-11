using GameCore.Grid;
using GameCore.Grid.Interfaces;
using GameCore.Grid.Settings;
using GameInfrastructure.Pathfinder;
using GameInfrastructure.Pathfinder.Interfaces;
using GameUnity.Infrastructure.Interfaces;
using GameUnity.Infrastructure.Services;
using GameUnity.Infrastructure.Services.Interfaces;
using GameUnity.Settings;
using UnityEngine;

namespace GameUnity.Infrastructure
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private GridSettings gridSettings;

        private IServiceLocator serviceLocator;
        public IServiceLocator ServiceLocator => serviceLocator;

        void Awake()
        {
            serviceLocator = new ServiceLocator();

            var inputs = new InputService();
            serviceLocator.Register<IInputService<Vector2>>(inputs);

            var gridConfig = new GridConfig(gridSettings.Width, gridSettings.Height, gridSettings.CellSize, gridSettings.MovementCostProvider);
            var gridSystem = new GridSystem(gridConfig);
            serviceLocator.Register<IGridSystem>(gridSystem);

            var gridBlackboard = new GridBlackboard(gridSystem);
            serviceLocator.Register<IGridOccupancy>(gridBlackboard);

            var pathfinder = new AStarPathfinder(gridSystem);
            serviceLocator.Register<IPathfinder>(pathfinder);

            DontDestroyOnLoad(gameObject);
        }
    }
}