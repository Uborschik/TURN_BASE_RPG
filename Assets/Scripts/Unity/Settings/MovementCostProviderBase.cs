using GameCore.Grid.Interfaces;
using UnityEngine;

namespace GameUnity.Settings
{
    public abstract class MovementCostProviderBase : ScriptableObject, IMovementCostProvider
    {
        public abstract float GetMovementCost(int x, int y);
    }
}
