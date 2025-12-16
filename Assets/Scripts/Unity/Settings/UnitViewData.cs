using GameCore.Utils.Positions;
using System;
using UnityEngine;

namespace GameUnity.Settings
{
    [Serializable]
    public struct UnitStats
    {
        public int Agility;

        public UnitStats(int agility = 5)
        {
            Agility = agility;
        }
    }

    [Serializable]
    public class UnitViewData
    {
        public string Name;
        public UnitStats Stats;

        public int Initiative => Stats.Agility;
    }
}