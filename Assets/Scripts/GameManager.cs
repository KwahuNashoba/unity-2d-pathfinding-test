using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // TODO: make it singleton or get another solution

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
        
        gameState = new GameState(new Vector2Int(options.gridSize, options.gridSize), options.totalObstacles);

        mapGenerator.GenerateMap(options, gameState);

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
                Tilemap tilemap = a.CreateTilemap(mapGenerator.Grid, blankTilemap);
                StartCoroutine(a.FindPath(tilemap, new Tile()));
            }
        }
    }
}