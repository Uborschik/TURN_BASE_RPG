using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameUnity.Settings
{
    [CreateAssetMenu(fileName = "GridViewSettings", menuName = "Game/Settings/GridView")]
    public class GridViewSettings : ScriptableObject
    {
        public TileBase WalkableTile;
        public TileBase BlockedTile;
    }
}