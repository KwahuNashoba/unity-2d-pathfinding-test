
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BackgroundTilesFactory", menuName = "Pathfinding 2D/Tile generators/Background")]
public class BackgroundTilemapFactory : AbstractTilemapFactory
{
    // TODO: this should be moved to abstract class so all maps can have it
    private Tilemap secondaryTilemap;
    public BackgroundTilemapFactory(Transform parentGrid, GameObject tilemapTemplate)
        : base(parentGrid, tilemapTemplate) {}

    protected override string GenerateTilemapName()
    {
        return "Watter";
    }

    protected override void PopulateTilemap(GameState gameState, MapTileSprites spriteSet)
    {
        secondaryTilemap = CreateTilemap(tilemap.transform, tilemap.gameObject);
        Vector3Int mapSize = new Vector3Int(gameState.GridSize.x, gameState.GridSize.y * 16 / 9 + 4, 0);
        PopulatePrimaryMap(mapSize, spriteSet);
        PopulateSecondaryMap(mapSize, spriteSet);
    }

    private void PopulatePrimaryMap(Vector3Int size, MapTileSprites spriteSet)
    {
        tilemap.size = size;
        Tile primaryBackgroundTile = new Tile();
        primaryBackgroundTile.sprite = spriteSet.BackgourndPrimary;
        for(int i = -4 ; i < size.x + 4; ++i)
        {
            for(int j = - size.y / 2 ; j < size.y; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), primaryBackgroundTile);
            }
        }
    }

    private void PopulateSecondaryMap(Vector3Int size, MapTileSprites spriteSet)
    {
        secondaryTilemap.size = size;
        Tile secondaryBackgroundTile = new Tile();
        secondaryBackgroundTile.sprite = spriteSet.BackgoroundSecondary;
        // 20th of backgroudn will be socondary
        for(int i = 0; i < size.x * size.y / 20; ++i)
        {
            var randomPosition = new Vector3Int(
                Random.Range(-4, size.x + 4),
                Random.Range(-size.y / 2, size.y),
                0
            );
            secondaryTilemap.SetTile(randomPosition, secondaryBackgroundTile);
        }
    }
}