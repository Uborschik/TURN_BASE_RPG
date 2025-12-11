using GameUnity.Adapters.Interfaces;
using GameUnity.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameUnity.Adapters
{
    public class TilemapAdapter : IGridRenderer
    {
        private readonly Tilemap tilemap;
        private readonly TileBase walkableTile;
        private readonly TileBase blockedTile;

        public TilemapAdapter(Tilemap tilemap, GridViewSettings settings)
        {
            this.tilemap = tilemap;
            walkableTile = settings.WalkableTile;
            blockedTile = settings.BlockedTile;
        }

        public void DrawCell(int x, int y, bool isWalkable)
        {
            var tile = isWalkable ? walkableTile : blockedTile;
            tilemap.SetTile(new Vector3Int(x, y, 0), tile);
        }

        public void Clear()
        {
            tilemap.ClearAllTiles();
        }
    }
}