using System.Linq;

public abstract class PathfinderState<CollectionType, NodeType>
    where NodeType : PathfinderNode
{
    public CollectionType WalkableNodes { get; set; }
    public CollectionType ObstacleNodes { get; set; }
    public NodeType startNode { get; set; }
    public NodeType endNode { get; set; }

    public abstract void ImportGameState(GameState gameState);
    public abstract CollectionType GetNeighbors(NodeType node);
    public abstract int GetCost(NodeType startNode, NodeType endNode);
}