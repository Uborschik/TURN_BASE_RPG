using GameCore.Grid;
using GameCore.Grid.Settings;
using GameCore.Utils.Positions;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class GridSystemTest
    {
        [Test]
        public void Constructor_CreatesCorrectSizeGrid()
        {
            var config = new GridConfig(width: 10, height: 5, cellSize: 1f);

            var system = new GridSystem(config);

            Assert.AreEqual(10, system.Config.Width);
            Assert.AreEqual(5, system.Config.Height);
            Assert.AreEqual(50, system.Nodes.Length);
        }

        [Test]
        public void GetCell_ReturnsCorrectCell()
        {
            var config = new GridConfig(5, 5, 1f);
            var grid = new GridSystem(config);

            var cell = grid.GetCell(new Position2Int(2, 3));

            Assert.IsNotNull(cell);
            Assert.AreEqual(2, cell.X);
            Assert.AreEqual(3, cell.Y);
            Assert.AreEqual(1f, cell.MovementCost);
        }

        [Test]
        public void GetCell_OutOfBounds_ReturnsNull()
        {
            var config = new GridConfig(5, 5, 1f);
            var grid = new GridSystem(config);

            var cell = grid.GetCell(new Position2Int(10, 10));
            Assert.IsNull(cell);
        }

        [Test]
        public void Cell_HasCorrectNeighborsCount()
        {
            var config = new GridConfig(5, 5, 1f);
            var grid = new GridSystem(config);

            var centerCell = grid.GetCell(new Position2Int(2, 2));
            Assert.AreEqual(8, centerCell.Neighbours.Count);

            var cornerCell = grid.GetCell(new Position2Int(0, 0));
            Assert.AreEqual(3, cornerCell.Neighbours.Count);

            var edgeCell = grid.GetCell(new Position2Int(2, 0));
            Assert.AreEqual(5, edgeCell.Neighbours.Count);
        }
    }
}