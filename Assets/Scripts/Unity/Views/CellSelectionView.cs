using System;
using UnityEngine;

namespace GameUnity.Views
{
    [Serializable]
    public class SelectionTransforms
    {
        public Transform Highlight;
        public Transform Select;
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

            highlightedTileObj.gameObject.SetActive(true);

            highlightedTileObj.position = position;
            previousHighlightPosition = position;
        }

        public void Select(Vector3 position)
        {
            if (position == previousSelectPosition) return;

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