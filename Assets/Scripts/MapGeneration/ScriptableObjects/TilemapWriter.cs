using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TilemapWriter : ScriptableObject
{
    protected Tilemap tilemap;

    // TODO: make this somehow auto-call
    // TODO: you might want to change Grid type with Transform
    public void CreateTilemap(
        Grid parentGrid,
        GameObject tilemapTemplate
    )
    {
        GameObject tilemapObject = Instantiate(tilemapTemplate, parentGrid.gameObject.transform);
        tilemapObject.name = GenerateTilemapName();
        tilemap = tilemapObject.GetComponent<Tilemap>();
    }

    protected abstract string GenerateTilemapName();
}