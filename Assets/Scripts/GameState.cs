using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Vector2Int GridSize { get; set; }
    public IList<Vector2Int> Walkables { get; private set; }
    public IList<Vector2Int> Obstacles { get; private set; }
    public Vector2Int Start { get; private set; }
    public Vector2Int End { get; private set; }

    public bool GenerateNewState(Vector2Int start, Vector2Int end, Vector2Int gridSize, int obstacleCount)
    {
        GridSize = gridSize;

        // check if start/end are inside the grid and that obstacle cound is less then total grid fields
        bool optionsValid = true;
        optionsValid &= PositionInsideBounds(start.x, start.y, GridSize.x, GridSize.y);
        optionsValid &= PositionInsideBounds(end.x, end.y, GridSize.x, GridSize.y);
        optionsValid &= obstacleCount < gridSize.x * gridSize.y;
        
        if (!optionsValid) return false;

        Start = start;
        End = end;
        GenerateWalkableFields(gridSize);
        GenerateRandomObstacles(gridSize, obstacleCount);

        // TODO: check if generate state is valid, if not, regenerate it
        return true;
    }

    private bool PositionInsideBounds(int x, int y, int xLength, int yLength)
    {
        return x >= 0 && x < xLength && y >= 0 && y < yLength;
    }
    
    private void GenerateWalkableFields(Vector2Int gridSize)
    {
        Walkables = new List<Vector2Int>(gridSize.x * gridSize.y);
        for(int i = 0; i < gridSize.x; ++i)
        {
            for(int j = 0; j < gridSize.y; ++j)
            {
                Walkables.Add(new Vector2Int(i, j));
            }
        }
    }

    private void GenerateRandomObstacles(Vector2Int gridSize, int obstacleCount)
    {
        Obstacles = new List<Vector2Int>(obstacleCount);

        while (Obstacles.Count < obstacleCount)
        {
            var obstaclePosition = new Vector2Int(
                Random.Range(0, gridSize.x - 1),
                Random.Range(0, gridSize.y - 1)
            );
            
            if(!Obstacles.Contains(obstaclePosition) && Start != obstaclePosition && End != obstaclePosition)
            {
                Obstacles.Add(obstaclePosition);
                Walkables.Remove(obstaclePosition);
            }
        }
    }
}