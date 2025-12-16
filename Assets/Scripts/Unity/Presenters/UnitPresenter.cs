using GameCore.Models;
using GameCore.Services.Grid;
using GameCore.Services.Objects;
using GameUnity.Factories;
using GameUnity.Settings;
using GameUnity.ViewModels;
using GameUnity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnity.Presenters
{
    public class UnitPresenter
    {
        private readonly UnitFactory factory;
        private readonly GridView gridView;
        private readonly GridObjectPlacer placer;
        private readonly UnitTurnOrderService order;
        private readonly GridObjectRegistry objects;

        public event Action<UnitView> UnitViewCreated;
        public event Action<UnitViewModel> UnitUpdated;

        public UnitPresenter(
            UnitFactory factory,
            GridView gridView,
            GridObjectPlacer placer,
            UnitTurnOrderService order,
            GridObjectRegistry objects)
        {
            this.factory = factory;
            this.gridView = gridView;
            this.placer = placer;
            this.order = order;
            this.objects = objects;
        }

        public void InitializeUnits()
        {
            var allUnits = new List<UnitPair>();

            allUnits.AddRange(factory.Create(Team.Player));
            allUnits.AddRange(factory.Create(Team.Enemy));

            var models = new List<UnitModel>(allUnits.Count);

            foreach (var unit in allUnits)
                models.Add(unit.Model);

            Debug.Log("=== Неотсортированный список ===");
            foreach (var m in models)
                Debug.Log($"ID: {m.Id.Value}, Initiative: {m.Initiative}");

            placer.InitializePositions(models);
            order.RegisterUnits(models);

            Debug.Log("\n=== Отсортированный список ===");
            var sortedOrder = order.GetCurrentOrder();
            foreach (var m in sortedOrder)
                Debug.Log($"ID: {m.Id.Value}, Initiative: {m.Initiative}");

            foreach (var unit in allUnits)
                UnitViewCreated?.Invoke(unit.View);
        }

        public void NotifyUnitUpdated(GridObjectId unitId)
        {
            var vm = GetUnitViewModel(unitId);
            UnitUpdated?.Invoke(vm);
        }

        public UnitViewModel GetUnitViewModel(GridObjectId unitId)
        {
            var model = objects.GetByType(ObjectType.Unit)
                .OfType<UnitModel>()
                .FirstOrDefault(u => u.Id == unitId);

            if (model == null || !model.Position.HasValue)
            {
                Debug.LogError($"[Presenter] Unit {unitId.Value} not found!");
                return default;
            }

            return new UnitViewModel(
                id: model.Id,
                worldPos: gridView.GridToWorld(model.Position.Value),
                isAlive: model.IsAlive,
                mp: model.MovementPoints
            );
        }
    }

    public readonly struct UnitPair
    {
        public UnitView View { get; }
        public UnitModel Model { get; }

        public UnitPair(UnitView view, UnitModel model)
        {
            View = view;
            Model = model;
        }
    }
}