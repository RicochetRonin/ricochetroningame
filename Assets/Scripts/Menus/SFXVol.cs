using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXVol : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetVol(float vol)
    {
        mixer.SetFloat("SfmVol", Mathf.Log10(vol) * 20);
    }
}
