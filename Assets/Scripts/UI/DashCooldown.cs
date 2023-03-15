using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashCooldown : MonoBehaviour
{
    public TextMeshProUGUI dashCooldown;
    //Attaches to the DashCooldown component, child of UI

    public void SetCooldown(bool canDash)
    {
        dashCooldown.text = "Dash Ready: " + canDash.ToString();
    }
}
