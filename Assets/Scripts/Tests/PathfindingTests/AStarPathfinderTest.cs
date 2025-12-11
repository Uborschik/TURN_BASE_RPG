using GameCore.Grid;
using GameCore.Grid.Settings;
using GameCore.Utils.Positions;
using GameInfrastructure.Pathfinder;
using GameInfrastructure.Pathfinder.Interfaces;
using NUnit.Framework;

namespace Tests
{
    public class AStarPathfinderTest
    {
        private GridSystem _gridSystem;
        private IPathfinder _pathfinder;

        [SetUp]
        public void Setup()
        {
            var config = new GridConfig(10, 10, 1f);
            _gridSystem = new GridSystem(config);

            _pathfinder = new AStarPathfinder(_gridSystem);
        }

        [Test]
        public void FindPath_StraightLine_ReturnsCorrectPath()
        {
            // Arrange: Старт (0,0) → Цель (5,0)
            var start = new Position2Int(0, 0);
            var end = new Position2Int(5, 0);

            // Act: Ищем путь
            var path = _pathfinder.FindPath(start, end);

            // Assert: Должно быть 6 клеток (включая старт)
            Assert.AreEqual(6, path.Count);
            Assert.AreEqual(new Position2Int(0, 0), path[0]);
            Assert.AreEqual(new Position2Int(5, 0), path[5]);
        }

        [Test]
        public void FindPath_WithWall_FindsAlternativePath()
        {
            // Arrange: Стена блокирует прямой путь
            _gridSystem.GetCell(new Position2Int(3, 0)).MovementCost = 0f;

            var start = new Position2Int(0, 0);
            var end = new Position2Int(5, 0);

            // Act
            var path = _pathfinder.FindPath(start, end);

            // Assert: Путь найден, но обходит стену
            Assert.IsNotEmpty(path);
            Assert.AreEqual(new Position2Int(5, 0), path[^1]); // Конечная точка достигнута

            // Проверяем, что обошли стену
            CollectionAssert.DoesNotContain(path, new Position2Int(3, 0)); // Не прошли через стену
            CollectionAssert.Contains(path, new Position2Int(3, 1)); // Пошли вверх
        }

        [Test]
        public void FindPath_AroundWall_ReturnsAlternativePath()
        {
            // Arrange: Строим стену, но можно обойти сверху
            for (int x = 2; x <= 4; x++)
                _gridSystem.GetCell(new Position2Int(x, 0)).MovementCost = 0f;

            var start = new Position2Int(0, 0);
            var end = new Position2Int(5, 1);

            // Act: Ищем путь
            var path = _pathfinder.FindPath(start, end);

            // Assert: Путь должен идти через Y=1
            Assert.Greater(path.Count, 0);
            CollectionAssert.Contains(path, new Position2Int(2, 1)); // Обходит стену
        }

        [Test]
        public void HasLineOfSight_ClearPath_ReturnsTrue()
        {
            // Arrange
            var from = new Position2Int(0, 0);
            var to = new Position2Int(5, 0);

            // Act
            bool hasLOS = _pathfinder.HasLineOfSight(from, to, maxRange: 10);

            // Assert
            Assert.IsTrue(hasLOS);
        }

        [Test]
        public void HasLineOfSight_OutOfRange_ReturnsFalse()
        {
            // Arrange
            var from = new Position2Int(0, 0);
            var to = new Position2Int(10, 10);

            // Act
            bool hasLOS = _pathfinder.HasLineOfSight(from, to, maxRange: 5);

            // Assert
            Assert.IsFalse(hasLOS); // Слишком далеко
        }
    }
}