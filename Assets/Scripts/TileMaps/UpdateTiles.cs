using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateTiles : MonoBehaviour
{
    public Tilemap midgroundTileMap;
    public Tile tile;
    public bool isEnabled;
    public List<Vector3Int> positions = new List<Vector3Int>();


    [ContextMenu("RemoveMidgroundTile")]
    //Used for removing tiles from the midground
    //The background should already be painted
    public void RemoveMidgroundTile()
    {
        foreach (Vector3Int position in positions)
        {
            //Remove Midground Tile
            midgroundTileMap.SetTile(position, null);
        }
        isEnabled = true;
    }

    public void AddMidgroundTile()
    {
        foreach (Vector3Int position in positions)
        {
            //Add Midground Tile
            midgroundTileMap.SetTile(position, tile);
        }
        isEnabled = false;
    }
}
