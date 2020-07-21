
using UnityEngine;
using UnityEngine.UI;

public class PathfinderResultViewholder : MonoBehaviour
{
    public Text formatedResult;

    public void PopulateView(PathfinderResult result)
    {
        string results = "";
        foreach (var r in result.AlgorithmResults)
        {
            results +=$"\n\t- {r.AlgorithmName} t: {r.TotalTimeElapsed} s: {r.TotalFieldsInspected}";
        }

        formatedResult.text = $"" +
            $"\n{result.RunNumber + 1}#" +
            $"\ngrid: {result.BoardSize}x{result.BoardSize}" +
            $"\nobstacles: {result.ObstacleCount}" +
            $"\nAlgorithms:{results}";
    }
}
