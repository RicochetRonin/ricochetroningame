using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Omni_Manager : MonoBehaviour
{

    public TextMeshProUGUI omni_countdown_text;
    public TextMeshProUGUI omni_ready_text;

    private Animator animator;

    public int loopCount = 0;

    public int countDownTime;
    public int resetCountDownTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        omni_countdown_text.enabled = false;
        omni_ready_text.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void omniActivate()
    {
        omni_ready_text.enabled = false;
        animator.SetTrigger("WindUp");
    }

    public void omniReady()
    {
        omni_countdown_text.enabled = false;
        omni_ready_text.enabled = true;
    }

    public IEnumerator startCountDown(int time)
    {
        animator.SetTrigger("WindUp");
        omni_countdown_text.enabled = true;
        omni_ready_text.enabled = false;
        countDownTime = time;
        omni_countdown_text.text = countDownTime.ToString();
        while (countDownTime > 0)
        {
            omni_countdown_text.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
    }

    public IEnumerator startResetCountDown(int time)
    {
        animator.SetTrigger("CoolDown");
        omni_countdown_text.enabled = true;
        resetCountDownTime = time;
        omni_countdown_text.text = resetCountDownTime.ToString();
        while (resetCountDownTime > 0)
        {
            omni_countdown_text.text = resetCountDownTime.ToString();
            yield return new WaitForSeconds(1f);
            resetCountDownTime--;
        }

        omni_countdown_text.enabled = false;
        
    }

}
