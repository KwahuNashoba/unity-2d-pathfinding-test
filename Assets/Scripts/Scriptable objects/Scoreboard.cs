using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Scoreboard", menuName = "Pathfinding 2D/Data holders/Scoreboard")]
public class Scoreboard : ScriptableObject
{
    public IList<PathfinderResult> Results { 
        get { 
            if(results == null)
            {
                results = new List<PathfinderResult>();
            }
            return results;
        }
        private set { results = value; } }
    [SerializeField] public IList<PathfinderResult> results;

    public void AddResult(PathfinderResult result)
    {
        Results.Add(result);
    }
}