using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParams : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameManager gameManager;

    private void Awake()
    {
        playerMovement = gameManager.findPlayerMovement();
    }
    public void setPlayerMoveSpeed()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setSpeed(newSpeed);
    }

    public void setJumpVelocity()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setJumpVelocity(newSpeed);
    }

    public void setMaxJumps()
    {
        int newSpeed = int.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setMaxJumps(newSpeed);
    }
    public void setFallMultiplier()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setFallMultiplier(newSpeed);
    }

    public void setLowJumpMultiplier()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setLowJumpMultiplier(newSpeed);
    }

    public void setDashForce()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setDashForce(newSpeed);
    }

    public void setDashTime()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setDashTime(newSpeed);
    }

    public void setDashCooldown()
    {
        float newSpeed = float.Parse(gameObject.GetComponent<TMPro.TMP_InputField>().text);
        playerMovement.setDashCooldown(newSpeed);
    }
}
