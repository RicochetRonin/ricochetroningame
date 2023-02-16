using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private static CinemachineVirtualCamera vCam;
    private static CinemachineBasicMultiChannelPerlin perlin;
    private static float shakeTimer;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimer <= 0)
            return;

        shakeTimer -= Time.unscaledDeltaTime;

        if (shakeTimer > 0)
            return;

        perlin.m_AmplitudeGain = 0;
    }

    public static void Shake(float duration = 0.2f, float intensity = 2.5f)
    {
        if (perlin.m_AmplitudeGain > intensity)
            return;

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }
}
