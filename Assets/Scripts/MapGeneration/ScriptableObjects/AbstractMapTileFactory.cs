using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractTilemapFactory : ScriptableObject
{
    // NOTE: you might want to change Grid type with Transform
    public void GenerateTilemap(
        Grid parentGrid,    
        GameObject tileMapTemplate,
        IList<Vector3Int> tilePositions,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    )
    {
        // each factory generates new child "sprite layer" and then populates it with tiles
        GameObject tilemapObject = Instantiate(tileMapTemplate, parentGrid.gameObject.transform);
        tilemapObject.name = GenerateTilemapName();
        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();

        PopulateTilemap(tilePositions, tilemap, spriteSet, gameOptions);
    }

    protected abstract string GenerateTilemapName();

    protected abstract void PopulateTilemap(
        IList<Vector3Int> tilePositions,
        Tilemap tileMap,
        MapTileSprites spriteSet,
        OptionSettings gameOptions
    );
}