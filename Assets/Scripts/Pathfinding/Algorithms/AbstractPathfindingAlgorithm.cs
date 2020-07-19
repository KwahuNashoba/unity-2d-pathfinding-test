using System;
using System.Collections;
using UnityEngine;

// NOTE: this could have been implemented as .NET POCO class
// but being ScriptableObject lets game designer play with different algorithms with no developer intervention
// and should be even possible to download new algorithm from server without altering original code
public abstract class AbstractPathfindingAlgorithm : ScriptableObject
{
    [SerializeField]
    public Sprite RunnerSprite;
    [SerializeField]
    public Sprite PathfindingMarking;

    // NOTE: this havely rellies on the fact that state is static
    // and it's safe for each path finder to recreate it's own version of state

    public IEnumerator ScheduleAndRun(
        GameState gameState,
        Action<bool> finishCallback,
        Action<Vector3Int> runnerPositionUpdated = null,
        Action<Vector3Int> nodeInspectedCallback = null)
    {
        Init(gameState);
        yield return FindPath(gameState, finishCallback, runnerPositionUpdated, nodeInspectedCallback);
    }

    protected abstract void Init(GameState gameState);
    protected abstract IEnumerator FindPath(
        GameState gameState,
        Action<bool> finishCallback,
        Action<Vector3Int> runnerPositionUpdated = null,
        Action<Vector3Int> nodeInspectedCallback = null);

}