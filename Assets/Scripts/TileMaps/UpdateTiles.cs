using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateTiles : MonoBehaviour
{
    public Tilemap midgroundTileMap;
    public Tile tile;
    public bool isEnabled;
    public List<Vector3Int> positions = new List<Vector3Int>();
    public Sprite disabledSprite;
    public Sprite enabledSprite;
    public AudioClip omniReadySFX;

    private SpriteRenderer interactableSpriteRenderer;

    public void Start()
    {
        interactableSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    [ContextMenu("RemoveMidgroundTile")]
    //Used for removing tiles from the midground
    //The background should already be painted
    public void RemoveMidgroundTile()
    {
        interactableSpriteRenderer.sprite = enabledSprite;   
        foreach (Vector3Int position in positions)
        {
            //Remove Midground Tile
            midgroundTileMap.SetTile(position, null);
        }
        AudioManager.PlayOneShotSFX(omniReadySFX);
        isEnabled = true;
    }

    public void AddMidgroundTile()
    {
        interactableSpriteRenderer.sprite = disabledSprite;
        foreach (Vector3Int position in positions)
        {
            //Add Midground Tile
            midgroundTileMap.SetTile(position, tile);
        }
        isEnabled = false;
    }
}
