using GameCore.Models;
using UnityEngine;

namespace GameUnity.ViewModels
{
    public readonly struct UnitViewModel
    {
        public readonly GridObjectId Id;
        public readonly Vector3 WorldPosition;
        public readonly bool IsAlive;
        public readonly int MovementPoints;
        public readonly bool IsSelected;

        public UnitViewModel(GridObjectId id, Vector3 worldPos, bool isAlive, int mp, bool isSelected = false)
        {
            Id = id;
            WorldPosition = worldPos;
            IsAlive = isAlive;
            MovementPoints = mp;
            IsSelected = isSelected;
        }
    }
}