using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // TODO: make it singleton or get another solution

    [SerializeField]
    private OptionSettings options;
    [SerializeField]
    private Grid levelGrid;
    [SerializeField]
    private GameObject blankTilemap;
    [SerializeField]
    private List<AbstractPathfindingAlgorithm> pathfinders;


    void Start()
    {
        OnButtonGoClicked(); // TODO: remove this
    }

    void Update()
    {
        
    }

    public void OnButtonGoClicked()
    {
        StartPathfinders();
    }

    private void StartPathfinders()
    {
        if (pathfinders.Count > 0)
        {
            foreach (var a in pathfinders)
            {
                Tilemap tilemap = a.CreateTilemap(levelGrid, blankTilemap);
                StartCoroutine(a.FindPath(tilemap, new Tile()));
            }
        }
    }
}