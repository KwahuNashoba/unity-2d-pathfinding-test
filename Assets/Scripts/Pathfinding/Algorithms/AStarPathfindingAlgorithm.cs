using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AStarPathfinder", menuName = "Pathfinding 2D/Algorithms/AStar")]
public class AStarPathfindingAlgorithm : AbstractPathfindingAlgorithm
{
    private AStarPathfinderState state;

    public override string GetName()
    {
        return "A Star";
    }

    protected override IEnumerator FindPath(
        GameState gameState,
        Action<bool> finishCallback,
        Action<Vector3Int> runnerPositionUpdated = null,
        Action<Vector3Int> nodeInspectedCallback = null)
    {
        var openNodes = new List<AStarNode>();
        var closedNodes = new List<AStarNode>();

        openNodes.Add(state.startNode);

        while (openNodes.Any())
        {
            openNodes.Sort();

            var currentNode = openNodes[0];

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

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

                int newNeighborDistance = currentNode.gCost + state.GetCost(currentNode, n);
                if (n.fCost > newNeighborDistance || !openNodes.Contains(n))
                {
                    n.parent = currentNode;
                    n.gCost = newNeighborDistance;
                    n.hCost = state.GetCost(n, state.endNode);

                    if(!openNodes.Contains(n))
                    {
                        openNodes.Add(n);
                    }
                }
            }

            yield return null;
        }

        finishCallback(false);
    }

    protected override void Init(GameState gameState)
    {
        state = new AStarPathfinderState();
        state.ImportGameState(gameState);
    }


    ///////////////////////////////////////////////////////////////////////////////
    // NOTE: these classes are inside this class so they can be dynamically loaded, 
    // as assets, without altering original code of built executable
    ///////////////////////////////////////////////////////////////////////////////
    
    // TODO: implement heap instead of list to optimize sorting
    private class AStarPathfinderState : PathfinderState<IList<AStarNode>, AStarNode>
    {
        private Vector2Int gridSize;

        public override int GetCost(AStarNode startNode, AStarNode endNode)
        {
            int diagonalDst = 14, streightDst = 10;
            int xDst = Mathf.Abs(endNode.x - startNode.x);
            int yDst = Mathf.Abs(endNode.y - startNode.y);

            if(xDst < yDst)
            {
                return diagonalDst * xDst + (yDst - xDst) * streightDst;
            } else
            {
                return diagonalDst * yDst + (xDst - yDst) * streightDst;
            }

        }

        public override IList<AStarNode> GetNeighbors(AStarNode node)
        {
            var neighbors = new List<Vector2Int>();
            for(int x = -1; x <= 1; ++x)
            {
                for(int y = -1; y <= 1; ++y)
                {
                    Vector2Int neighborPosition = new Vector2Int(node.x + x,node.y + y);
                    if( neighborPosition.x >= 0 && neighborPosition.x < gridSize.x && neighborPosition.y >= 0 && neighborPosition.y < gridSize.y)
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
            WalkableNodes = gameState.Walkables.AsQueryable().Select(w => new AStarNode(w)).ToList();
            ObstacleNodes = gameState.Obstacles.AsQueryable().Select(o => new AStarNode(o)).ToList();
            startNode = new AStarNode(gameState.Start);
            endNode = new AStarNode(gameState.End);
        }
    }

    private class AStarNode : PathfinderNode, IComparable
    {
        public int gCost, hCost;
        public int fCost { get { return hCost + gCost; } }

        public AStarNode(Vector2Int position) : base(position) {}

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            AStarNode compareWith = (AStarNode)obj;
            if (fCost < compareWith.fCost || fCost == compareWith.fCost && hCost < compareWith.hCost)
            {
                return -1;
            }
            else if (fCost > compareWith.fCost || fCost == compareWith.fCost && hCost > compareWith.hCost)
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