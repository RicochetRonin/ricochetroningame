using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private int levelToLoad;
    public CutsceneDialogue cutscene;

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscene.allowFade)
        {
            FadeToLevel(levelToLoad);
        }
    }
}
