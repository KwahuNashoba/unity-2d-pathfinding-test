using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Scoreboard", menuName = "Pathfinding 2D/Data holders/Scoreboard")]
public class Scoreboard : ScriptableObject
{
    public IList<PathfinderResult> Results { get; private set; }

    private void Awake()
    {
        Results = new List<PathfinderResult>();
    }

    public void AddResult(PathfinderResult result)
    {
        Results.Add(result);
    }
}