using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateTiles : MonoBehaviour
{
    public Tile newTile;
    public Vector3Int position;
    public Tilemap backgroundTileMap;
    public Tilemap midgroundTileMap;

    [ContextMenu("Paint")]
    void Paint()
    {
        //Remove Previous Tile
        midgroundTileMap.SetTile(position, null);

        //Set new Tile
        backgroundTileMap.SetTile(position, newTile);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Trigger!");
            Paint();
        }
        else
        {
            Debug.Log("Non player activated trigger");
        }
    }
}
