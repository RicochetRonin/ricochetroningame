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
        //Debug.Log(other.tag);
        if (other.CompareTag("PlayerBullet"))
        {
            GameObject particle = Instantiate(_hitParticleSytem, other.transform.position, other.transform.rotation);
            
            //AudioManager.PlayOneShotSFX(playSFX);
            
            particle.GetComponent<ParticleSystem>().Play();
        }
    }
}
