using System.Collections;
using UnityEngine;

// NOTE: this could have been implemented as .NET POCO class
// but being ScriptableObject lets game designer play with different algorithms with no developer intervention
// and should be even possible to download new algorithm from server without altering original code
public abstract class AbstractPathfindingAlgorithm : TilemapWriter
{
    [SerializeField]
    protected Sprite runnerSprite;
    [SerializeField]
    protected Sprite pathfindingMarking;

    // NOTE: this havely rellies on the fact that state is static
    // and it's safe for each path finder to recreate it's own version of state
    public abstract void ImportState(GameState gameState);
    public abstract IEnumerator FindPath(GameState gameState);

}