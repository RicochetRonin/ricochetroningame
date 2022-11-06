using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource, _bgmSource;

    [SerilaizeField] private AudioClip bgmToPlay;
    
    public void PlayOneShotSFX(AudioClip sfxToPlay)
    {
        _sfxSource.PlayOneShot(sfxToPlay);
    }

    void Start()
    {
        PlayBGM(bgmToPlay);
    }

    public void PlayBGM(AudioClip bgm)
    {
        //_bgmSource.Play(bgm);
    }
}
