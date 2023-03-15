using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OmniReflect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _hitParticleSytem;

    [SerializeField] private AudioClip playSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            GameManager.bulletsReflected++;
            GameObject particle = Instantiate(_hitParticleSytem, other.transform.position, other.transform.rotation);
                       
            particle.GetComponent<ParticleSystem>().Play();
        }

        //OmniReflect hit an interactable object
        if (other.CompareTag("Interactable"))
        {
            if (other.GetComponent<UpdateTiles>().isEnabled)
            {
                //Add tiles to the midground
                other.GetComponent<UpdateTiles>().AddMidgroundTile();
            }
            else
            {
                //Removes tiles from midground
                other.GetComponent<UpdateTiles>().RemoveMidgroundTile();
            }
        }
    }
}
