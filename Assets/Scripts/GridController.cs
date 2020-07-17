using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private OptionSettings options;
    [SerializeField]
    private MapTileSprites tileSprites;
    [SerializeField]
    private GameObject tileMapPrefab;
    [SerializeField]
    private List<AbstractTilemapFactory> tileFactories;
    
    // TODO: greed generation can be extracted to method for more flexable controll over generation
    void Start()
    {
        // TODO: move this somewhere else
        transform.position = new Vector3(-options.gridSize/2, -options.gridSize/2, 0);
        Camera.main.orthographicSize = options.gridSize + 2; // TODO: make side margins width independent of tile size/count


        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {
        foreach(AbstractTilemapFactory f in tileFactories)
        {
            f.GenerateTilemap(GetComponent<Grid>(), tileMapPrefab, new List<Vector3Int>(), tileSprites, options);
        }
    }
}
