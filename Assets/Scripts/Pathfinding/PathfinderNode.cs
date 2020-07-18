using UnityEngine;

public abstract class PathfinderNode
{
    public int x, y;
    public PathfinderNode parent { get; set; }
    public PathfinderNode(Vector2Int position)
    {
        x = position.x;
        y = position.y;
    }
}
