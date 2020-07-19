using UnityEngine;

[CreateAssetMenu(fileName = "LevelOptions", menuName = "Pathfinding 2D/Options", order = 1)]
[System.Serializable]
public class OptionSettings : ScriptableObject
{
    public int gridSize = 10;
    public Vector2Int startPosition = new Vector2Int(0, 4);
    public Vector2Int endPosition = new Vector2Int(9, 4);
    public int totalObstacles = 18;
}
