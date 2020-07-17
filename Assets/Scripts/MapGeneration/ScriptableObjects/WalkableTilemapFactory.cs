
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WalkableTilesFactory", menuName = "Pathfinding 2D/Tile generators/Walkable", order = 1)]
class WalkableTilemapFactory : AbstractTilemapFactory
{
    protected override string GenerateTilemapName()
    {
        return "Walkable";
    }

    protected override void PopulateTilemap(
        IList<Vector3Int> tilePositions,
        Tilemap tileMap,
        MapTileSprites spriteSet,
        OptionSettings gameOptions)
    {
        tileMap.size = new Vector3Int(gameOptions.gridSize + 2, gameOptions.gridSize + 2, 0);

        Tile tile = CreateInstance<Tile>();
        int minEdgeIndex = -1, maxEdgeIndex = gameOptions.gridSize;
        // populate edge tiles
        for(int i = 0; i < maxEdgeIndex; ++i)
        {

            // left edge
            tile.sprite = spriteSet.LeftEdge;
            tileMap.SetTile(new Vector3Int(minEdgeIndex, i, 0), tile);

            // right edge
            tile.sprite = spriteSet.RightEdge;
            tileMap.SetTile(new Vector3Int(maxEdgeIndex, i, 0), tile);

            // top edge
            tile.sprite = spriteSet.TopEdge;
            tileMap.SetTile(new Vector3Int(i, maxEdgeIndex, 0), tile);

            // bottom edge
            tile.sprite = spriteSet.BottomEdge;
            tileMap.SetTile(new Vector3Int(i, minEdgeIndex, 0), tile);
        }

        // populate corners
        tile.sprite = spriteSet.TopLeftCorner;
        tileMap.SetTile(new Vector3Int(minEdgeIndex, maxEdgeIndex, 0), tile);
        tile.sprite = spriteSet.TopRightCorner;
        tileMap.SetTile(new Vector3Int(maxEdgeIndex, maxEdgeIndex, 0), tile);
        tile.sprite = spriteSet.BottomLeftCorner;
        tileMap.SetTile(new Vector3Int(minEdgeIndex, minEdgeIndex, 0), tile);
        tile.sprite = spriteSet.BottomRightCorner;
        tileMap.SetTile(new Vector3Int(maxEdgeIndex, minEdgeIndex, 0), tile);


        // pupulate rest of tiles
        tile.sprite = spriteSet.Walkable;
        tileMap.FloodFill(Vector3Int.zero, tile);

    }
}
