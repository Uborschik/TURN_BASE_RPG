using GameCore.Models;

namespace GameCore.Services.Grid
{
    public class GridBlackboard
    {
        public GridSystem System { get; }
        public GridObjectRegistry Objects { get; }

        public GridBlackboard(GridSystem system)
        {
            System = system;
            Objects = new GridObjectRegistry(system);
        }
    }
}