using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource _sfxSource;

    void Awake()
    {
        _sfxSource = transform.GetChild(0).GetComponent<AudioSource>();
    }
    
    public static void PlayOneShotSFX(AudioClip sfxToPlay)
    {
        _sfxSource.PlayOneShot(sfxToPlay);
    }
}
