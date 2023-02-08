using System.Collections;
using UnityEngine;

public class SleepManager : MonoBehaviour
{
    #region Simple Singleton

    public static SleepManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    public static int isSleepingCount = 1;

    public static void Sleep(int frames)
    {
        instance.StartCoroutine(SleepOverTime(frames / isSleepingCount));
    }

    public static IEnumerator SleepOverTime(int frames)
    {
        Time.timeScale = 0.001f;
        isSleepingCount++;

        for (int i = 0; i < frames; i++)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        Time.timeScale = 1f;
        isSleepingCount--;
    }
}
