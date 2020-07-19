using UnityEngine;
using UnityEngine.Tilemaps;

// TODO: classes extending this one should be refactored towards composition instead of inheretance
public abstract class TilemapWriter : ScriptableObject
{
    protected Tilemap tilemap;

    public TilemapWriter(Transform parentGrid, GameObject tilemapTemplate)
    {
        tilemap = CreateTilemap(parentGrid, tilemapTemplate);
    }

    // used to initialize state of loaded scriptable objects because
    // they don't get to call constructors
    public void Init(Transform parentGrid, GameObject tilemapTemplate)
    {
        tilemap = CreateTilemap(parentGrid, tilemapTemplate);
    }

    public Tilemap CreateTilemap(
        Transform parentGrid,
        GameObject tilemapTemplate
    )
    {
        GameObject tilemapObject = Instantiate(tilemapTemplate, parentGrid);
        tilemapObject.name = GenerateTilemapName();
        return tilemapObject.GetComponent<Tilemap>();
    }

    protected abstract string GenerateTilemapName();
}