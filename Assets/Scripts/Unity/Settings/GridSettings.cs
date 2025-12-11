using GameCore.Grid;
using GameCore.Grid.Settings;
using UnityEngine;

namespace GameUnity.Settings
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "Game/Settings/Grid")]
    public class GridSettings : ScriptableObject
    {
        public int Width = 20;
        public int Height = 20;
        public float CellSize = 1f;

        [SerializeReference] public MovementCostProviderBase MovementCostProvider;

        public GridConfig ToGridConfig()
        {
            return new GridConfig(Width, Height, CellSize, MovementCostProvider);
        }
    }
}