using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractTilemapFactory : TilemapWriter
{
    public void WriteTiles(
        Grid parentGrid,    
        GameObject tilemapTemplate,
        IList<Vector3Int> tilePositions,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    )
    {
        // each factory generates new child "sprite layer" and then populates it with tiles
        Tilemap tilemap = CreateTilemap(parentGrid, tilemapTemplate);

        PopulateTilemap(tilePositions, tilemap, spriteSet, gameOptions);
    }

    protected abstract void PopulateTilemap(
        IList<Vector3Int> tilePositions,
        Tilemap tilemap,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    );
}