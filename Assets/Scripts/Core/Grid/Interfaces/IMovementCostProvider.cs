namespace GameCore.Grid.Interfaces
{
    public interface IMovementCostProvider
    {
        float GetMovementCost(int x, int y);
    }
}