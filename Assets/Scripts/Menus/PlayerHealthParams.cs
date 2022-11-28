using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthParams : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public GameManager gameManager;

    private void Awake()
    {
        playerHealth  = gameManager.findPlayerHealth();
    }

    public void setPlayerMaxHP()
    {
        float newHealth = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerHealth.setMaxHealth(newHealth);
    }

    public void setCanTakeDamage(bool canDamage)
    {
        playerHealth.setCanTakeDamage(canDamage);
    }
}
