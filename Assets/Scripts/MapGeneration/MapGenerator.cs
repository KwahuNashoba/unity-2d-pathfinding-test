using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class MapGenerator : MonoBehaviour
{
    private Grid grid;
    public Grid Grid
    {
        get { return grid ?? GetComponent<Grid>(); }
        private set { grid = value; } 
    }

    [SerializeField]
    private MapTileSprites tileSprites;
    [SerializeField]
    private GameObject tileMapPrefab;
    [SerializeField]
    private List<AbstractTilemapFactory> tileFactories;

    public void GenerateMap(OptionSettings options, GameState gameState)
    {
        // TODO: move these somewhere else
        transform.position = new Vector3(-options.gridSize / 2, -options.gridSize / 2, 0);
        Camera.main.orthographicSize = options.gridSize + 2; // TODO: make side margins width independent of tile size/count

        foreach(AbstractTilemapFactory f in tileFactories)
        {
            f.WriteTiles(Grid, tileMapPrefab, gameState, tileSprites, options);
        }
    }
}
