using GameCore.Models;
using GameUnity.Presenters;
using GameUnity.ViewModels;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace GameUnity.Views
{
    public class UnitViewRegistry : IInitializable, IDisposable
    {
        private readonly UnitPresenter presenter;
        private readonly Dictionary<GridObjectId, UnitView> views = new();

        public UnitViewRegistry(UnitPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Initialize()
        {
            presenter.UnitViewCreated += RegisterView;
            presenter.UnitUpdated += UpdateView;

            presenter.InitializeUnits();
        }

        public void Dispose()
        {
            presenter.UnitViewCreated -= RegisterView;
            presenter.UnitUpdated -= UpdateView;
        }

        private void RegisterView(UnitView view)
        {
            views[view.Id] = view;
            var vm = presenter.GetUnitViewModel(view.Id);
            UpdateView(vm);
            view.Transform.gameObject.SetActive(true);
        }

        private void UpdateView(UnitViewModel vm)
        {
            if (!views.TryGetValue(vm.Id, out var view)) return;

            view.Transform.position = vm.WorldPosition;
            view.Renderer.sortingOrder = 20 - (int)vm.Id.Value;
            view.Transform.gameObject.SetActive(vm.IsAlive);
        }
    }
}