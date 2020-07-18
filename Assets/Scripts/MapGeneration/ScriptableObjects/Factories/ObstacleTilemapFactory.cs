using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ObstacleTilesFactory", menuName = "Pathfinding 2D/Tile generators/Obstacle")]
public class ObstacleTilemapFactory : AbstractTilemapFactory
{
    protected override string GenerateTilemapName()
    {
        return "Obstacles";
    }

    protected override void PopulateTilemap(GameState gameState, MapTileSprites spriteSet, OptionSettings gameOptions)
    {
        tilemap.size = new Vector3Int(gameOptions.gridSize + 2, gameOptions.gridSize + 2, 0);
        Tile tile = CreateInstance<Tile>();
        tile.sprite = spriteSet.Obstacle;

        foreach(var o in gameState.Obstacles)
        {
            tilemap.SetTile(new Vector3Int(o.x, o.y, 0), tile);
        }

    }
}