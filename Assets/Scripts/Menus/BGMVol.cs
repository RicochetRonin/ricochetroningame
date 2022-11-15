using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMVol : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetVol(float vol)
    {
        mixer.SetFloat("BgmVol", Mathf.Log10(vol) * 20);
    }
}
