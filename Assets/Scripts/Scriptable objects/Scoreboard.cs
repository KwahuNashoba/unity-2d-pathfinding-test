using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Scoreboard", menuName = "Pathfinding 2D/Data holders/Scoreboard")]
public class Scoreboard : ScriptableObject
{
    private void OnEnable()
    {
        // Since this object is referenced in game scene only, it's mutated state gets unloaded
        // once the main scene gets unloaded, leaving this instance with 0 refs.
        // This is quick solution to make it stay in memory
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public IList<PathfinderResult> Results { 
        get { 
            if(results == null)
            {
                results = new List<PathfinderResult>();
            }
            return results;
        }
        private set { results = value; } }
    private IList<PathfinderResult> results;

    public void AddResult(PathfinderResult result)
    {
        Results.Add(result);
    }
}