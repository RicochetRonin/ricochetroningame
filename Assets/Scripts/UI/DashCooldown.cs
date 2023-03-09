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
        // 1. Need to grab current input action? And always display
        // 2. Replace True and False with the sprite
        //dashCooldown.text = "Dash Ready: "+canDash.ToString();
        dashCooldown.text = "           " + canDash.ToString();
    }
}
