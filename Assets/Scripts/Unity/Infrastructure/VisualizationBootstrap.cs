using GameUnity.Adapters;
using GameUnity.Infrastructure.Interfaces;
using GameUnity.Infrastructure.Services.Interfaces;
using GameUnity.Settings;
using GameUnity.Views;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameUnity.Infrastructure
{
    [RequireComponent(typeof(CompositionRoot))]
    public class VisualizationBootstrap : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap mainTilemap;
        [SerializeField] private LineRenderer linePrefab;
        [SerializeField] private Transform highlightedTileObj;
        [SerializeField] private Transform selectedTileObj;
        [SerializeField] private GridViewSettings gridViewSettings;
        [SerializeField] private PathViewSettings pathViewSettings;

        private IServiceLocator serviceLocator;
        private GlobalView globalView;
        private IInputService<Vector2> inputService;

        private bool isMouseMoved;
        private Vector2 lastScreenPosition;
        private Vector2 worldMousePosition;

        private void Awake()
        {
            serviceLocator = GetComponent<CompositionRoot>().ServiceLocator;
            inputService = serviceLocator.Get<IInputService<Vector2>>();

            var tilemapAdapter = new TilemapAdapter(mainTilemap, gridViewSettings);
            var gridView = new GridView(tilemapAdapter);
            var pathView = new PathView(linePrefab, pathViewSettings);
            var cellSelectionView = new CellSelectionView(highlightedTileObj, selectedTileObj);

            globalView = new GlobalView(serviceLocator, gridView, pathView, cellSelectionView);
        }

        private void Start()
        {
            globalView.OnStart();
        }

        private void OnEnable()
        {
            inputService.LeftClick += HandleClick;
            inputService.RightClick += globalView.ResetPoints;
        }

        private void OnDisable()
        {
            inputService.LeftClick -= HandleClick;
            inputService.RightClick -= globalView.ResetPoints;
        }

        private void Update()
        {
            var currentScreenPos = inputService.GetScreenPosition();

            isMouseMoved = currentScreenPos != lastScreenPosition;

            if (isMouseMoved)
            {
                lastScreenPosition = currentScreenPos;
                worldMousePosition = mainCamera.ScreenToWorldPoint(currentScreenPos);

                globalView.HighlightCell(worldMousePosition);
            }
        }

        private void HandleClick()
        {
            var position = inputService.GetScreenPosition();
            worldMousePosition = mainCamera.ScreenToWorldPoint(position);

            globalView.OnClick(worldMousePosition);
        }
    }
}