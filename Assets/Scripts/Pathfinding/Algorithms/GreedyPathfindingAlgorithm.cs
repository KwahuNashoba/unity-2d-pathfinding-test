using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GreedyPathfinder", menuName = "Pathfinding 2D/Algorithms/Greedy")]
public class GreedyPathfindingAlgorithm : AbstractPathfindingAlgorithm
{
    private GreedyPathFinderState state;

    public override string GetName()
    {
        return "Greedy";
    }

    protected override IEnumerator FindPath(GameState gameState, Action<bool> finishCallback, Action<Vector3Int> runnerPositionUpdated = null, Action<Vector3Int> nodeInspectedCallback = null)
    {
        var openNodes = new List<GreedyNode>();
        var closedNodes = new List<GreedyNode>();

        openNodes.Add(state.startNode);

        while (openNodes.Any())
        {
            openNodes.Sort();
            var currentNode = openNodes[0];
            closedNodes.Add(currentNode);
            openNodes.Remove(currentNode);

            runnerPositionUpdated?.Invoke(new Vector3Int(currentNode.x, currentNode.y, 0));

            if (currentNode.x == state.endNode.x && currentNode.y == state.endNode.y)
            {
                finishCallback(true);
                yield break;
            }

            foreach (var n in state.GetNeighbors(currentNode))
            {
                if (closedNodes.Contains(n)) continue;

                nodeInspectedCallback?.Invoke(new Vector3Int(n.x, n.y, 0));

                if (!openNodes.Contains(n))
                {
                    n.hCost = state.GetCost(n, state.endNode);
                    openNodes.Add(n);
                }
            }

            yield return null;
        }

        finishCallback(false);
    }

    protected override void Init(GameState gameState)
    {
        state = new GreedyPathFinderState();
        state.ImportGameState(gameState);
    }

    private class GreedyPathFinderState : PathfinderState<IList<GreedyNode>, GreedyNode>
    {
        private Vector2Int gridSize;

        public override int GetCost(GreedyNode startNode, GreedyNode endNode)
        {
            int diagonalDst = 14, streightDst = 10;
            int xDst = Mathf.Abs(endNode.x - startNode.x);
            int yDst = Mathf.Abs(endNode.y - startNode.y);

            if (xDst < yDst)
            {
                return diagonalDst * xDst + (yDst - xDst) * streightDst;
            }
            else
            {
                return diagonalDst * yDst + (xDst - yDst) * streightDst;
            }

        }

        public override IList<GreedyNode> GetNeighbors(GreedyNode node)
        {
            var neighbors = new List<Vector2Int>();
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    Vector2Int neighborPosition = new Vector2Int(node.x + x, node.y + y);
                    if (neighborPosition.x >= 0 && neighborPosition.x < gridSize.x && neighborPosition.y >= 0 && neighborPosition.y < gridSize.y)
                    {
                        neighbors.Add(neighborPosition);
                    }
                }
            }
            // remove node itself
            neighbors.Remove(new Vector2Int(node.x, node.y));

            return WalkableNodes.Where(w => neighbors.Contains(new Vector2Int(w.x, w.y))).ToList();
        }

        public override void ImportGameState(GameState gameState)
        {
            gridSize = gameState.GridSize;
            WalkableNodes = gameState.Walkables.AsQueryable().Select(w => new GreedyNode(w)).ToList();
            ObstacleNodes = gameState.Obstacles.AsQueryable().Select(o => new GreedyNode(o)).ToList();
            startNode = new GreedyNode(gameState.Start);
            endNode = new GreedyNode(gameState.End);
        }
    }

    private class GreedyNode : PathfinderNode, IComparable
    {
        public int hCost { get; set; }
        public GreedyNode(Vector2Int position) : base(position)
        { }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            GreedyNode compareWith = (GreedyNode)obj;
            if (hCost < compareWith.hCost)
            {
                return -1;
            }
            else if (hCost > compareWith.hCost)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}