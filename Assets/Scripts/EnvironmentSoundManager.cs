using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSoundManager : MonoBehaviour
{

    /// <summary>
    /// This is an attempted audio manager for the environment and enemies.
    /// I have abandoned it after examining the old manager.
    /// </summary>
    [SerializeField] private static AudioSource source;


    [Header("Audio Clips")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip pulseSFX;
    [SerializeField] AudioClip sniperSFX;
    [SerializeField] AudioClip bounceSFX;
    [SerializeField] AudioClip hitPlayerSFX;
    [SerializeField] AudioClip reflectedSFX;

    public void Shoot()
    {
        source.PlayOneShot(shootSFX);
    }
    public void Pulse()
    {
        source.PlayOneShot(pulseSFX);
    }
    public void Snipe()
    {
        source.PlayOneShot(sniperSFX);
    }
    public void Bounce()
    {
        source.PlayOneShot(bounceSFX);
    }
    public void HitPlayer()
    {
        source.PlayOneShot(hitPlayerSFX);
    }
    public void Reflected()
    {
        source.PlayOneShot(reflectedSFX);
    }
}
