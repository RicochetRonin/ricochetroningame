using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OmniCooldown : MonoBehaviour
{
    //Attaches to the DashCooldown component, child of UI
    public TextMeshProUGUI omniCooldown;

    public void SetCooldown(bool canOmni)
    {
        omniCooldown.text = "Omni Ready: " + canOmni.ToString();
    }
}
