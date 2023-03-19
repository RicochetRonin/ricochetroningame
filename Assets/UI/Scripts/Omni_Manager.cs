using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Omni_Manager : MonoBehaviour
{

    public TextMeshProUGUI omni_countdown_text;

    private Animator animator;

    public int loopCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void omniActivate()
    {
        animator.SetTrigger("WindUp");
    }

    public int countdown(int timeLeft)
    {
      
        omni_countdown_text.text = timeLeft.ToString();
        timeLeft--;
        if (timeLeft < 1)
        {
            animator.SetTrigger("CoolDown");
        }
        return timeLeft;

    }

}
