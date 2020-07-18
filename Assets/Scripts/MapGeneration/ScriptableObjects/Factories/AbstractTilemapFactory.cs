using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractTilemapFactory : TilemapWriter
{
    public void WriteTiles(
        GameState gameState,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    )
    {
        PopulateTilemap(gameState, spriteSet, gameOptions);
    }

    protected abstract void PopulateTilemap(
        GameState gameState,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    );
}