using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "AStarPathfinder", menuName = "Pathfinding 2D/Algorithms/AStar")]
public class AStarPathfindingAlgorithm : AbstractPathfindingAlgorithm
{
    public override IEnumerator FindPath(Tilemap algorithmTilemap, Tile runnerTile)
    {
        Debug.Log("A* pathfinder started");
        yield break;
    }

    protected override string GenerateTilemapName()
    {
        return "AStar";
    }
}