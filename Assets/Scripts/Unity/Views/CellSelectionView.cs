using GameCore.Grid;
using GameCore.Utils.Positions;
using GameUnity.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

namespace GameUnity.Views
{
    public class SelectionTransforms
    {
        public Transform Highlight { get; }
        public Transform Select { get; }

        public SelectionTransforms(Transform highlight, Transform select)
        {
            Highlight = highlight;
            Select = select;
        }
    }

    public class CellSelectionView
    {
        private readonly Transform highlightedTileObj;
        private readonly Transform selectedTileObj;

        private Vector3 previousHighlightPosition;
        private Vector3? previousSelectPosition;

        public CellSelectionView(SelectionTransforms transforms)
        {
            highlightedTileObj = transforms.Highlight;
            selectedTileObj = transforms.Select;

            highlightedTileObj.gameObject.SetActive(false);
            selectedTileObj.gameObject.SetActive(false);
        }

        public void Highlight(Vector3 position)
        {
            if (position == previousHighlightPosition) return;

            if (!highlightedTileObj.gameObject.activeSelf)
                highlightedTileObj.gameObject.SetActive(true);

            highlightedTileObj.position = position;
            previousHighlightPosition = position;
        }

        public void Select(Vector3 position)
        {
            if (position == previousSelectPosition) return;

            if (!selectedTileObj.gameObject.activeSelf)
                selectedTileObj.gameObject.SetActive(true);

            selectedTileObj.position = position;
            previousSelectPosition = position;
        }

        public void ClearSelectTile()
        {
            selectedTileObj.gameObject.SetActive(false);
            previousSelectPosition = null;
        }
    }
}