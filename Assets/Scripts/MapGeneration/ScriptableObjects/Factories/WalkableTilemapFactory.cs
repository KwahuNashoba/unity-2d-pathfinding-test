using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WalkableTilesFactory", menuName = "Pathfinding 2D/Tile generators/Walkable")]
class WalkableTilemapFactory : AbstractTilemapFactory
{
    protected override string GenerateTilemapName()
    {
        return "Walkable";
    }

    protected override void PopulateTilemap( GameState gameState, MapTileSprites spriteSet, OptionSettings gameOptions)
    {
        tilemap.size = new Vector3Int(gameOptions.gridSize + 2, gameOptions.gridSize + 2, 0);

        Tile tile = CreateInstance<Tile>();
        int minEdgeIndex = -1, maxEdgeIndex = gameOptions.gridSize;
        // populate edge tiles
        for(int i = 0; i < maxEdgeIndex; ++i)
        {

            // left edge
            tile.sprite = spriteSet.LeftEdge;
            tilemap.SetTile(new Vector3Int(minEdgeIndex, i, 0), tile);

            // right edge
            tile.sprite = spriteSet.RightEdge;
            tilemap.SetTile(new Vector3Int(maxEdgeIndex, i, 0), tile);

            // top edge
            tile.sprite = spriteSet.TopEdge;
            tilemap.SetTile(new Vector3Int(i, maxEdgeIndex, 0), tile);

            // bottom edge
            tile.sprite = spriteSet.BottomEdge;
            tilemap.SetTile(new Vector3Int(i, minEdgeIndex, 0), tile);
        }

        // populate corners
        tile.sprite = spriteSet.TopLeftCorner;
        tilemap.SetTile(new Vector3Int(minEdgeIndex, maxEdgeIndex, 0), tile);
        tile.sprite = spriteSet.TopRightCorner;
        tilemap.SetTile(new Vector3Int(maxEdgeIndex, maxEdgeIndex, 0), tile);
        tile.sprite = spriteSet.BottomLeftCorner;
        tilemap.SetTile(new Vector3Int(minEdgeIndex, minEdgeIndex, 0), tile);
        tile.sprite = spriteSet.BottomRightCorner;
        tilemap.SetTile(new Vector3Int(maxEdgeIndex, minEdgeIndex, 0), tile);


        // pupulate rest of tiles
        tile.sprite = spriteSet.Walkable;
        tilemap.FloodFill(Vector3Int.zero, tile);

    }
}
