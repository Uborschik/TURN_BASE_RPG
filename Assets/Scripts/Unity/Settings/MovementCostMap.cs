using GameCore.Utils.Random;
using UnityEngine;

namespace GameUnity.Settings
{
    [CreateAssetMenu(fileName = "MovementCostProvider", menuName = "Game/MovementCostProviders/MovementCostProvider")]
    public class MovementCostMap : MovementCostProviderBase
    {
        public override float GetMovementCost(int x, int y)
        {
            if (y % 2 == 0 && x % 2 == 0)
                return Dice.RollChance(30) ? 1f : 0f;

            return 1f;
        }
    }
}
