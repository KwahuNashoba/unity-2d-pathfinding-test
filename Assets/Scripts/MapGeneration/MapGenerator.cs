using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Grid))]
public class MapGenerator : MonoBehaviour
{
    private Grid grid;
    public Grid Grid
    {
        get {
            if (grid == null)
            {
                grid = GetComponent<Grid>();
            }
            return grid;
        }
        private set { grid = value; } 
    }

    [SerializeField]
    private MapTileSprites tileSprites;
    [SerializeField]
    private GameObject tilemapPrefab;
    [SerializeField]
    private List<AbstractTilemapFactory> tileFactories;

    private UnityEvent CleanMapEvent;

    public void GenerateMap(GameState gameState)
    {
        var options = GameOptions.Options;

        CleanMap();
        PositionMap(options);
        FocusCameraOnMap(options);

        foreach(AbstractTilemapFactory f in tileFactories)
        {
            f.Init(transform, tilemapPrefab);
            f.WriteTiles(gameState, tileSprites);
            CleanMapEvent.AddListener(f.CleanTilemap);
        }
    }

    private void PositionMap(GameOptions options)
    {
        transform.position = new Vector3(-options.GridSize / 2, -options.GridSize / 4, 0);
    }

    private void FocusCameraOnMap(GameOptions options)
    {
        Camera.main.orthographicSize = options.GridSize + 2;
    }

    private void CleanMap()
    {
        if(CleanMapEvent == null)
        {
            CleanMapEvent = new UnityEvent();
        }

        CleanMapEvent.Invoke();
        CleanMapEvent.RemoveAllListeners();
    }
}
