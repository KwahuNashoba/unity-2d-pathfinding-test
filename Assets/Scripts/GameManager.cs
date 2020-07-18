using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private OptionSettings options;
    [SerializeField]
    private GameObject blankTilemap;
    [SerializeField]
    private List<AbstractPathfindingAlgorithm> pathfinders;

    private GameState gameState;

    void Start()
    {
        gameState = new GameState();
        
        bool stateGenerated = gameState.GenerateNewState(
            options.startPosition,
            options.endPosition,
            new Vector2Int(options.gridSize, options.gridSize),
            options.totalObstacles);

        if(!stateGenerated)
        {
            // TODO: notify about invalid options
        }
        else
        {
            mapGenerator.GenerateMap(options, gameState);
        }

        OnButtonGoClicked(); // TODO: remove this
    }

    public void OnButtonGoClicked()
    {
        StartPathfinders();
    }

    private void StartPathfinders()
    {
        if (pathfinders.Count > 0)
        {
            foreach (var a in pathfinders)
            {
                a.CreateTilemap(mapGenerator.Grid, blankTilemap);
                a.ImportState(gameState);
                StartCoroutine(a.FindPath(gameState));
            }
        }
    }
}