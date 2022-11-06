using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private static AudioSource _sfxSource;

    public static void PlayOneShotSFX(AudioClip sfxToPlay)
    {
        _sfxSource.PlayOneShot(sfxToPlay);
    }
}
