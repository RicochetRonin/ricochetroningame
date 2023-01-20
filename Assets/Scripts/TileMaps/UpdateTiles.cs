using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateTiles : MonoBehaviour
{
    public Tilemap midgroundTileMap;
    public List<Vector3Int> positions = new List<Vector3Int>();


    [ContextMenu("RemoveMidgroundTile")]
    //Used for removing tiles from the midground
    //The background should already be painted
    public void RemoveMidgroundTile()
    {
        foreach (Vector3Int position in positions){
            //Remove Midground Tile
            midgroundTileMap.SetTile(position, null);

        }
    }
}
