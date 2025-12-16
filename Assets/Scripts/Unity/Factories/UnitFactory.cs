using GameCore.Models;
using GameCore.Utils.Positions;
using GameUnity.Presenters;
using GameUnity.Settings;
using GameUnity.Views;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnity.Factories
{
    public class UnitFactory
    {
        private readonly UnitsViewSettings settings;
        private readonly GameObject prefab;
        private uint nextUnitId = 1;

        public UnitFactory(UnitsViewSettings settings)
        {
            this.settings = settings;
            prefab = settings.Prefab;
        }

        public List<UnitPair> Create(Team team)
        {
            var result = new List<UnitPair>();
            var data = team switch
            {
                Team.Player => settings.PlayerTeam,
                Team.Enemy => settings.EnemyTeam,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

            foreach (var item in data)
            {
                var view = CreateView(item);
                var model = CreateModel(item, team, view.Id);
                result.Add(new UnitPair(view, model));
            }

            return result;
        }

        private UnitView CreateView(UnitViewData data)
        {
            var id = new GridObjectId(nextUnitId++, ObjectType.Unit);
            var obj = UnityEngine.Object.Instantiate(prefab);
            obj.name = $"{data.Name}_Unit{id}";
            obj.SetActive(false);
            var view = new UnitView(id, obj.transform, obj.transform.GetComponent<SpriteRenderer>());

            return view;
        }

        private UnitModel CreateModel(UnitViewData data, Team team, GridObjectId id)
        {
            var model = new UnitModel(id, data.Initiative, team);

            return model;
        }
    }
}