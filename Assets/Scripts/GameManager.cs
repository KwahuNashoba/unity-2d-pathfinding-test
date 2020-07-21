using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private GameObject tilemapTemplate;
    [SerializeField] private List<AbstractPathfindingAlgorithm> pathfinders;
    [SerializeField] private Scoreboard scoreboard;

    public UnityEvent<PathfinderResult> RunFinishedEvent { 
        get {
            if(runFinishedEvent == null)
            {
                runFinishedEvent = new UnityEvent<PathfinderResult>();
            }
            return runFinishedEvent;
        }
    }
    private UnityEvent<PathfinderResult> runFinishedEvent;

    public UnityEvent NewStateGeneratedEvent
    {
        get
        {
            if (newStateGeneratedEvent == null)
            {
                newStateGeneratedEvent = new UnityEvent();
            }
            return newStateGeneratedEvent;
        }
    }
    private UnityEvent newStateGeneratedEvent;

    private GameState gameState;
    private GameOptions options;
    private int totalRunnersActive;
    private PathfinderResult currentResult;
    private UnityEvent StateCleanEvent;


    void Start()
    {
        options = GameOptions.Options;

        GenerateNewState();
    }

    public void GenerateNewState()
    {
        gameState = new GameState();
        bool stateGenerated = gameState.GenerateNewState(
            options.StartPosition,
            options.EndPosition,
            new Vector2Int(options.GridSize, options.GridSize),
            options.TotalObstacles,
            pathfinders[0]); // TODO: there should be option to provide exact pathfinder

        if (!stateGenerated)
        {
            // TODO: notify about invalid options
        }
        else
        {
            InvokeStateCleanEvent();
            NewStateGeneratedEvent.Invoke();
            mapGenerator.GenerateMap(gameState);
        }
    }

    public void StartPathfinders()
    {
        totalRunnersActive = 0;

        currentResult = new PathfinderResult()
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
                currentResult.AlgorithmResults.Add(new AlgorithmResult());
                var runner = new AlgorithmRunner(
                    mapGenerator.transform,
                    tilemapTemplate,
                    a.RunnerSprite,
                    a.PathfindingMarking,
                    gameState,
                    currentResult.AlgorithmResults[currentResult.AlgorithmResults.Count - 1]
                );

                runner.OnFinish.AddListener(OnRunnerFinished);
                StateCleanEvent.AddListener(() => { DestroyImmediate(runner); });

                StartCoroutine(runner.Run(a));
                ++totalRunnersActive;
            }
        }
    }

    private void OnRunnerFinished()
    {
        if(--totalRunnersActive == 0)
        {
            //scoreboard.AddResult(currentResult);
            // increase number of obstacles after each run by one
            options.TotalObstacles++;
            RunFinishedEvent.Invoke(currentResult);
        }
    }

    private void InvokeStateCleanEvent()
    {
        if(StateCleanEvent == null)
        {
            StateCleanEvent = new UnityEvent();
        }
        StateCleanEvent.Invoke();
        StateCleanEvent.RemoveAllListeners();
    }
}