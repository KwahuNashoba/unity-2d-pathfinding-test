using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private GameObject tilemapTemplate;
    [SerializeField]
    private List<AbstractPathfindingAlgorithm> pathfinders;
    [SerializeField]
    private Button buttonGo;


    private GameState gameState;
    private GameOptions options;
    private Scoreboard scoreboard;

    void Start()
    {
        options = GameOptions.Options;
        scoreboard = new Scoreboard();
        
        gameState = new GameState();
        bool stateGenerated = gameState.GenerateNewState(
            options.StartPosition,
            options.EndPosition,
            new Vector2Int(options.GridSize, options.GridSize),
            options.TotalObstacles,
            pathfinders[0]); // TODO: there should be option to provide exact pathfinder

        if(!stateGenerated)
        {
            // TODO: notify about invalid options
        }
        else
        {
            mapGenerator.GenerateMap(gameState);
        }

        RegisterCallbacks();
    }

    public void OnButtonGoClicked()
    {
        StartPathfinders();
    }

    private void StartPathfinders()
    {
        PathfinderResult result = new PathfinderResult()
        {
            BoardSize = options.GridSize,
            ObstacleCount = options.TotalObstacles,
            RunNumber = scoreboard.Results.Count,
            AlgorithmResults = new List<AlgorithmResult>()
        };

        if (pathfinders.Count > 0)
        {
            foreach (var a in pathfinders)
            {
                result.AlgorithmResults.Add(new AlgorithmResult());
                var runner = new AlgorithmRunner(
                    mapGenerator.transform,
                    tilemapTemplate,
                    a.RunnerSprite,
                    a.PathfindingMarking,
                    gameState,
                    result.AlgorithmResults[result.AlgorithmResults.Count - 1]
                );

                StartCoroutine(runner.Run(a));
            }
        }
    }

    private void RegisterCallbacks()
    {
        buttonGo.onClick.AddListener(OnButtonGoClicked);
    }
}