using System.Collections.Generic;

public class PathfinderResult
{
    public int RunNumber { get; set; }
    public int BoardSize { get; set; }
    public int ObstacleCount { get; set; }
    public IList<AlgorithmResult> AlgorithmResults { get; set; }
}

public class AlgorithmResult
{ 
    public string AlgorithmName { get; set; }
    public int TotalFieldsInspected { get; set; }
    public float TotalTimeElapsed { get; set; }
    public bool PathFound { get; set; }
}

