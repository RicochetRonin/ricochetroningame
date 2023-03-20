using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health_Manager : MonoBehaviour
{

    public TextMeshProUGUI health_text;

    private Animation myAnimation;

    public AnimationClip health_loop;

    private Animator animator;

    public float frameRate;


    public void Start()
    {
        health_loop.frameRate = 2;

        myAnimation = GetComponent<Animation>();

        animator = GetComponent<Animator>();
    }


    private void Health_Update(float currentHealth)
    {
        frameRate = Mathf.Abs(20 - currentHealth);
        health_loop.frameRate = frameRate;
        // Set Loop speed to be dependant on currentHealth
    }
    public void PlayerHit(float currentHealth, float damageDealt)
    {
        //Debug.Log("Damage dealt " + damageDealt);
        float finalHealth = currentHealth - damageDealt;

        if (finalHealth < 0)
        {
            finalHealth = 0;
        }

        health_text.text = finalHealth.ToString();

        if (damageDealt == 2)
        {
            //Debug.Log("Player damage 2 anim");
            //myAnimation.Play("Heart_2HP_Hit");
            animator.SetTrigger("2HP");
            // Play 2HP Animation
        }
        else
        {
            //myAnimation.Play("Heart_5HP_Hit");
            animator.SetTrigger("5HP");
            // Play 5HP Animation
        }

        Health_Update(finalHealth);

    }

    public void SetHealth(float health)
    {
        health_text.text = health.ToString();
    }



}
