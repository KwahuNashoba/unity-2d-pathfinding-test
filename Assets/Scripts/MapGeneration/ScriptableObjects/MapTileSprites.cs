using UnityEngine;

[CreateAssetMenu(fileName = "MapSprites", menuName = "Pathfinding 2D/Map Tileset", order = 1)]
public class MapTileSprites : ScriptableObject
{
    public Sprite TopLeftCorner;
    public Sprite TopRightCorner;
    public Sprite BottomLeftCorner;
    public Sprite BottomRightCorner;

    public Sprite LeftEdge;
    public Sprite RightEdge;
    public Sprite TopEdge;
    public Sprite BottomEdge;

    public Sprite Walkable;
    public Sprite Obstacle;
}
