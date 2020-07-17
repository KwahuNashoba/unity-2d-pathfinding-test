using UnityEngine;

[CreateAssetMenu(fileName = "LevelOptions", menuName = "Pathfinding 2D/Options", order = 1)]
public class OptionSettings : ScriptableObject
{
    public int gridSize = 10;
    public Vector2 startPosition;
    public Vector2 endPosition;
}
