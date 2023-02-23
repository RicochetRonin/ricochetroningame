using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;
    [SerializeField] private float pixelsPerUnit, textureWidth, textureHeight;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    public bool manualTextureSize;
    

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        Debug.LogFormat("Width:{0}, Height: {1}, PPU: {2}", texture.width, texture.height, pixelsPerUnit);
        
        if (manualTextureSize)
        {
            textureUnitSizeX = textureWidth / pixelsPerUnit;
            textureUnitSizeY = textureHeight / pixelsPerUnit;
        }
        else
        {
            textureUnitSizeX = texture.width / pixelsPerUnit;
            textureUnitSizeY = texture.height / pixelsPerUnit;
        }
    }

    private void Update() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        //Debug.LogFormat("Delta Movement: {0}", deltaMovement);
        transform.position += new Vector3(deltaMovement.x * -parallaxEffectMultiplier.x, deltaMovement.y * -parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                //Debug.LogFormat("{0} is greater than {1}", cameraTransform.position.x - transform.position.x, textureUnitSizeX);
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                //Debug.LogFormat("Offset X Position: {0}", offsetPositionX);
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }
        
        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }
    }
}
