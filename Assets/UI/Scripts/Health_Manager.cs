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


    public void Start()
    {
        health_loop.frameRate = 15;

        myAnimation = GetComponent<Animation>();
        
    }

    private void Health_Update( float currentHealth )
    {
        health_loop.frameRate = Mathf.Abs(10 - currentHealth);
        // Set Loop speed to be dependant on currentHealth
    }
    public void PlayerHit( float currentHealth, float damageDealt )
    {
        float finalHealth = currentHealth - damageDealt;
        
        health_text.text = finalHealth.ToString();

        if ( damageDealt == 2 )
        {
            myAnimation.Play("Heart_2HP_Hit");
            // Play 2HP Animation
        }
        else
        {
            myAnimation.Play("Heart_5HP_Hit");
            // Play 5HP Animation
        }

        Health_Update(finalHealth);

    }

}
