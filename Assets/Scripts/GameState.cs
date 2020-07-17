using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class GameState
{
    public IList<Vector2Int> Walkables { get; private set; }
    public IList<Vector2Int> Obstacles { get; private set; }

    public GameState(Vector2Int gridSize, int obstacleCount)
    {
        GenerateNewState(gridSize, obstacleCount);
    }

    public void GenerateNewState(Vector2Int gridSize, int obstacleCount)
    {
        Walkables = GenerateWalkableFields(gridSize);
        Obstacles = GenerateRandomObstacles(gridSize, obstacleCount);

        Walkables = Walkables.Where(w => !Obstacles.Contains(w)).ToList();
    }
    
    private IList<Vector2Int> GenerateWalkableFields(Vector2Int gridSize)
    {
        var walkable = new List<Vector2Int>(gridSize.x * gridSize.y);
        for(int i = 0; i < gridSize.x; ++i)
        {
            for(int j = 0; j < gridSize.y; ++j)
            {
                walkable.Add(new Vector2Int(i, j));
            }
        }

        return walkable;
    }

    // TODO: can this be optimized
    private IList<Vector2Int> GenerateRandomObstacles(Vector2Int gridSize, int obstacleCount)
    {
        var obstacles = new List<Vector2Int>(obstacleCount);

        while (obstacles.Count < obstacleCount)
        {
            var obstaclePosition = new Vector2Int(
                Random.Range(0, gridSize.x - 1),
                Random.Range(0, gridSize.y - 1)
            );
            
            if(!obstacles.Contains(obstaclePosition))
            {
                obstacles.Add(obstaclePosition);
            }
        }

        return obstacles;
    }
}