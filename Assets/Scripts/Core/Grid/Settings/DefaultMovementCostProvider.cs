using GameCore.Grid.Interfaces;

namespace GameCore.Grid.Settings
{
    public class DefaultMovementCostProvider
    {
        public float GetMovementCost(int x, int y) => 1f;
    }
}