using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AlgorithmRunner : TilemapWriter
{
    private Tile runnerTile;
    private Tile pathfindingTile;
    private AlgorithmResult result;
    private GameState gameState;
    private float startTime;
    private Tilemap runnerTilemap;


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
        runnerTile = new Tile();
        runnerTile.sprite = runnerSprite;
        pathfindingTile = new Tile();
        pathfindingTile.sprite = pathfindingSprite;

        result = algorithmResult;
        gameState = gState;
        result = algorithmResult;

        // create addtional tilemap, as chiled of default one, so the runner moves on it
        runnerTilemap = CreateTilemap(tilemap.transform, tilemapTemplate);
        // and override the name
        runnerTilemap.name = $"{result.AlgorithmName} runner";
    }

    public IEnumerator Run(AbstractPathfindingAlgorithm algorithm)
    {
        startTime = Time.realtimeSinceStartup;
        yield return algorithm.ScheduleAndRun(gameState, OnFinish, OnRunnerMoved, OnNodeInspected);
    }

    protected override string GenerateTilemapName()
    {
        return $"Algorithm runner field inspector";
    }

    private void OnFinish(bool pathFound)
    {
        result.TotalTimeElapsed = Time.realtimeSinceStartup - startTime;
    }

    private void OnRunnerMoved(Vector3Int newPosition)
    {
        // TODO: this is probably expensive
        runnerTilemap.ClearAllTiles();
        runnerTilemap.SetTile(newPosition, runnerTile);
    }

    private void OnNodeInspected(Vector3Int nodePosition)
    {
        result.TotalFieldsInspected++;
        tilemap.SetTile(nodePosition, pathfindingTile);
    }
}