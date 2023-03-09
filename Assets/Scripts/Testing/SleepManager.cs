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

    public static float isSleepingCount = 1;

    public static void Sleep(int frames)
    {
        instance.StartCoroutine(SleepOverTime(frames));
    }

    private static IEnumerator SleepOverTime(int frames)
    {
        Time.timeScale = 0.001f;
        isSleepingCount++;

        yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime * (frames / isSleepingCount));

        Time.timeScale = 1f;
        isSleepingCount--;
    }
}
