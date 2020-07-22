
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "KeyElementsTilesFactory", menuName = "Pathfinding 2D/Tile generators/Key elements")]
public class KeyElementsFactory : AbstractTilemapFactory
{
    public KeyElementsFactory(Transform parentGrid, GameObject tilemapTemplate)
           : base(parentGrid, tilemapTemplate) { }

    protected override string GenerateTilemapName()
    {
        return "KeyElements";
    }

    protected override void PopulateTilemap(GameState gameState, MapTileSprites spriteSet)
    {
        Tile elementTile = new Tile();

        elementTile.sprite = spriteSet.startPosition;
        tilemap.SetTile(new Vector3Int(gameState.Start.x, gameState.Start.y, 0), elementTile);

        elementTile.sprite = spriteSet.endPosition;
        tilemap.SetTile(new Vector3Int(gameState.End.x, gameState.End.y, 0), elementTile);

        
    }
}