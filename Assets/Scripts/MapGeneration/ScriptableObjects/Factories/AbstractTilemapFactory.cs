using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractTilemapFactory : TilemapWriter
{
    public void WriteTiles(
        Grid parentGrid,    
        GameObject tilemapTemplate,
        GameState gameState,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    )
    {
        // each factory generates new child "sprite layer" and then populates it with tiles
        Tilemap tilemap = CreateTilemap(parentGrid, tilemapTemplate);

        PopulateTilemap(gameState, tilemap, spriteSet, gameOptions);
    }

    protected abstract void PopulateTilemap(
        GameState gameState,
        Tilemap tilemap,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    );
}