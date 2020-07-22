using UnityEngine;

[CreateAssetMenu(fileName = "MapSprites", menuName = "Pathfinding 2D/Map Tileset", order = 1)]

// TODO: those should all be tiles instead of sprites
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
    public Sprite BackgourndPrimary;
    public Sprite BackgoroundSecondary;

    public Sprite startPosition;
    public Sprite endPosition;
}
