using System.Collections;
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

    public UnityEvent<bool> StateGenerationFinished
    {
        get
        {
            if (stateGenerationFinished == null)
            {
                stateGenerationFinished = new UnityEvent<bool>();
            }
            return stateGenerationFinished;
        }
    }
    private UnityEvent<bool> stateGenerationFinished;

    private GameState gameState;
    private GameOptions options;
    private int totalRunnersActive;
    private PathfinderResult currentResult;
    private UnityEvent StateCleanEvent;


    void Start()
    {
        options = GameOptions.Options;

        StartCoroutine(GenerateNewState());
    }

    public IEnumerator GenerateNewState()
    {
        // TODO: add start and end state generation events so progress UI can be activated
        gameState = new GameState();
        
        int attempts = 10;
        // perform max 10 attempts to generate new state before calling it imposible
        do
        {
            bool stateGenerated = gameState.GenerateNewState(
                options.StartPosition,
                options.EndPosition,
                new Vector2Int(options.GridSize, options.GridSize),
                options.TotalObstacles);
            
            if(stateGenerated)
            {
                // TODO: there should be option to provide exact pathfinder
                yield return StartCoroutine(pathfinders[0].ScheduleAndRun(gameState, (pathFound) => 
                {
                    if (attempts == 1 && !pathFound)
                    {
                        StateGenerationFinished.Invoke(false);
                    }
                    else if (pathFound)
                    {
                        InvokeStateCleanEvent();
                        StateGenerationFinished.Invoke(true);
                        mapGenerator.GenerateMap(gameState);

                        // stop the loop
                        attempts = 1;
                    }
                }));
            }
            else
            {
                StateGenerationFinished.Invoke(false);
                yield break;
            }
        }
        while (--attempts > 0);
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