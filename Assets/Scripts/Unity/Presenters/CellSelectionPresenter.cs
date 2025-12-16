using GameCore.Interfaces;
using GameUnity.Services;
using GameUnity.Utils.Extensions;
using GameUnity.Views;
using System;
using UnityEngine;
using VContainer.Unity;

namespace GameUnity.Presenters
{
    public class CellSelectionPresenter : IInitializable, IDisposable
    {
        private readonly InputService inputs;
        private readonly CellSelectionView selectionView;

        public CellSelectionPresenter(InputService inputs, CellSelectionView selectionView)
        {
            this.inputs = inputs;
            this.selectionView = selectionView;

            inputs.WorldMousePosition += HighlightCell;
            inputs.LeftClick += SelectCell;
        }

        public void Initialize()
        {
            inputs.WorldMousePosition += HighlightCell;
            inputs.LeftClick += SelectCell;
        }

        public void Dispose()
        {
            inputs.WorldMousePosition -= HighlightCell;
            inputs.LeftClick -= SelectCell;
        }

        public void HighlightCell(Vector2 value)
        {
            selectionView.Highlight(value.ToCenter3D());
        }

        public void SelectCell()
        {
            var value = inputs.GetScreenPosition();

            selectionView.Select(value.ToCenter3D());
        }
    }
}