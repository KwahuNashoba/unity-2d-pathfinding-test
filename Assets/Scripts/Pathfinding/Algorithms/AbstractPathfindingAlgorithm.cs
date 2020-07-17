using System.Collections;
using UnityEngine.Tilemaps;

// NOTE: this could have been implemented as .NET POCO class
// but this way it lets game designer play with different algorithms with no developer intervention
// and should be even able to download new algorithm from server (never tried it though)
public abstract class AbstractPathfindingAlgorithm : TilemapWriter
{
    public abstract IEnumerator FindPath(Tilemap algorithmTilemap, Tile runnerTile);
}