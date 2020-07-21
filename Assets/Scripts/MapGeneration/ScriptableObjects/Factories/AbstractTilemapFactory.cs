using UnityEngine;

public abstract class AbstractTilemapFactory : TilemapWriter
{
    public AbstractTilemapFactory(Transform parentGrid, GameObject tilemapTemplate)
        : base (parentGrid, tilemapTemplate) { }

    public void WriteTiles(
        GameState gameState,
        MapTileSprites spriteSet
    )
    {
        PopulateTilemap(gameState, spriteSet);
    }
    public override void CleanTilemap()
    {
        tilemap.ClearAllTiles();
    }

    protected abstract void PopulateTilemap(
        GameState gameState,
        MapTileSprites spriteSet
    );
}