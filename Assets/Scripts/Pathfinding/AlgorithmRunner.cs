using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class AlgorithmRunner : TilemapWriter
{
    public UnityEvent OnFinish { get; set; }

    private Tile runnerTile;
    private Tile pathfindingTile;
    private Tilemap runnerTilemap;

    private GameState gameState;
    private AlgorithmResult result;

    private float startTime;

    public AlgorithmRunner(
        Transform destinationGrid,
        GameObject tilemapTemplate,
        Sprite runnerSprite,
        Sprite pathfindingSprite,
        GameState gState,
        AlgorithmResult algorithmResult
    )
        : base(destinationGrid, tilemapTemplate)
    {
        CreateTiles(runnerSprite, pathfindingSprite);
        
        gameState = gState;
        result = algorithmResult;

        OnFinish = new UnityEvent();

        // TODO: move this somewhere
        // create addtional tilemap, as chiled of default one, so the runner moves on it
        runnerTilemap = CreateTilemap(tilemap.transform, tilemapTemplate);
        // and override the name
        runnerTilemap.name = $"{result.AlgorithmName} runner";

    }

    public IEnumerator Run(AbstractPathfindingAlgorithm algorithm)
    {
        startTime = Time.realtimeSinceStartup;
        yield return algorithm.ScheduleAndRun(gameState, OnPathfindingFinished, OnRunnerMoved, OnNodeInspected);
    }

    protected override string GenerateTilemapName()
    {
        return $"Algorithm runner field inspector";
    }

    private void OnPathfindingFinished(bool pathFound)
    {
        result.TotalTimeElapsed = Time.realtimeSinceStartup - startTime;
        OnFinish.Invoke();
    }

    private void OnRunnerMoved(Vector3Int newPosition)
    {
        // TODO: this might be expensive
        runnerTilemap.ClearAllTiles();
        runnerTilemap.SetTile(newPosition, runnerTile);

    }

    private void OnNodeInspected(Vector3Int nodePosition)
    {
        result.TotalFieldsInspected++;
        tilemap.SetTile(nodePosition, pathfindingTile);
    }

    private void CreateTiles( Sprite runnerSprite, Sprite pathfindingSprite)
    {
        runnerTile = new Tile();
        runnerTile.sprite = runnerSprite;
        pathfindingTile = new Tile();
        pathfindingTile.sprite = pathfindingSprite;
    }

    public override void CleanTilemap()
    {
        // do nothing, the whole runner will be destroyed once the state is generated
        // this could be optimised to reuse same runner???
    }
}