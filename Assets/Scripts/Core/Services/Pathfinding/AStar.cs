using GameCore.Interfaces;
using GameCore.Utils.Collections;
using GameCore.Utils.Positions;
using System;
using System.Collections.Generic;

namespace GameCore.Services.Pathfinding
{
    public class AStar
    {
        private readonly INavigationNode[,] nodes;
        private readonly Heap<INavigationNode> openSet;
        private uint pathfindingVersion = 1;

        private const int DEFAULT_DIAGONAL_COST = 14;
        private const int DEFAULT_GENERAL_COST = 10;
        private const int MAX_PATH_LENGTH_DEFAULT = 10000;
        private const int OPEN_SET_CAPACITY_FACTOR = 10;
        private const int MIN_OPEN_SET_CAPACITY = 128;

        private readonly int diagonalCost;
        private readonly int generalCost;
        private readonly int maxPathLength;

        public AStar(INavigationNode[,] nodes, int diagonalCost = DEFAULT_DIAGONAL_COST,
                     int generalCost = DEFAULT_GENERAL_COST, int maxPathLength = MAX_PATH_LENGTH_DEFAULT)
        {
            this.nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            this.diagonalCost = diagonalCost;
            this.generalCost = generalCost;
            this.maxPathLength = maxPathLength;
            this.openSet = new Heap<INavigationNode>(EstimateOpenSetCapacity());
        }

        public List<Position2Int> FindPath(INavigationNode startNode, INavigationNode endNode)
        {
            if (!IsWalkable(startNode) || !IsWalkable(endNode))
                return new List<Position2Int>();

            if (ReferenceEquals(startNode, endNode))
                return new List<Position2Int> { new Position2Int(startNode.X, startNode.Y) };

            IncrementVersion();
            InitializeStartAndEnd(startNode, endNode);

            openSet.Clear();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var node = openSet.RemoveFirst();

                if (ReferenceEquals(node, endNode))
                    return RetracePath(startNode, endNode);

                node.IsInClosedSet = true;

                if (node.Neighbours == null) continue;

                foreach (var neighbour in node.Neighbours)
                {
                    if (neighbour == null) continue;
                    if (!IsWalkable(neighbour)) continue;

                    bool isInOpenSet = openSet.Contains(neighbour);

                    if (neighbour.Version == pathfindingVersion && neighbour.IsInClosedSet)
                        continue;

                    if (neighbour.Version != pathfindingVersion)
                    {
                        neighbour.ResetPathData();
                        neighbour.Version = pathfindingVersion;
                    }

                    var newCostToNeighbour = CalculateCostToNeighbour(node, neighbour);

                    if (newCostToNeighbour < neighbour.G || !isInOpenSet)
                    {
                        UpdateNodeData(neighbour, newCostToNeighbour, endNode, node);

                        if (!isInOpenSet)
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }

            return new List<Position2Int>();
        }

        private void InitializeStartAndEnd(INavigationNode startNode, INavigationNode endNode)
        {
            startNode.ResetPathData();
            endNode.ResetPathData();

            startNode.G = 0;
            startNode.H = GetDistance(startNode, endNode);
            startNode.Parent = null;
            startNode.Version = pathfindingVersion;
            endNode.Version = pathfindingVersion;
        }

        private int CalculateCostToNeighbour(INavigationNode fromNode, INavigationNode toNode)
        {
            int distance = GetDistance(fromNode, toNode);
            return fromNode.G + (int)(distance * toNode.MovementCost);
        }

        private void UpdateNodeData(INavigationNode node, int newCost, INavigationNode endNode, INavigationNode parent)
        {
            node.G = newCost;
            node.H = GetDistance(node, endNode);
            node.Parent = parent;
        }

        private void IncrementVersion()
        {
            pathfindingVersion++;
            if (pathfindingVersion == 0)
            {
                pathfindingVersion = 1;
                ResetAllNodes();
            }
        }

        private void ResetAllNodes()
        {
            foreach (var node in nodes)
            {
                if (node != null)
                {
                    node.ResetPathData();
                    node.Version = 0;
                }
            }
        }

        private bool IsWalkable(INavigationNode node) => node?.MovementCost > 0;

        private int EstimateOpenSetCapacity()
        {
            var estimatedSize = Math.Max(MIN_OPEN_SET_CAPACITY, nodes.Length / OPEN_SET_CAPACITY_FACTOR);
            return Math.Min(estimatedSize, Heap<INavigationNode>.MAXIMUM_CAPACITY);
        }

        private List<Position2Int> RetracePath(INavigationNode startNode, INavigationNode endNode)
        {
            var path = new List<Position2Int>();
            var currentNode = endNode;
            var safetyCounter = 0;

            while (currentNode != null && safetyCounter++ < maxPathLength)
            {
                path.Add(new Position2Int(currentNode.X, currentNode.Y));
                if (ReferenceEquals(currentNode, startNode)) break;
                currentNode = currentNode.Parent;
            }

            if (path.Count == 0 || path[^1] != new Position2Int(startNode.X, startNode.Y))
                path.Add(new Position2Int(startNode.X, startNode.Y));

            path.Reverse();
            return path;
        }

        private int GetDistance(INavigationNode a, INavigationNode b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException(a == null ? nameof(a) : nameof(b));

            var dstX = Math.Abs(a.X - b.X);
            var dstY = Math.Abs(a.Y - b.Y);

            return dstX > dstY
                ? diagonalCost * dstY + generalCost * (dstX - dstY)
                : diagonalCost * dstX + generalCost * (dstY - dstX);
        }
    }
}