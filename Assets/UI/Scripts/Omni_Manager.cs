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

    public int countDownTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        omni_countdown_text.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void omniActivate()
    {
        animator.SetTrigger("WindUp");
    }

    public IEnumerator startCountDown(int time)
    {
        Debug.Log("countdown");
        animator.SetTrigger("CoolDown");
        omni_countdown_text.enabled = true;
        countDownTime = time;
        while (countDownTime > 0)
        {
            omni_countdown_text.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        omni_countdown_text.enabled = false;
    }

}
