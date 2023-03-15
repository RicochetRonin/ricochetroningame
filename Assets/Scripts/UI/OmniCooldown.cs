using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OmniCooldown : MonoBehaviour
{
    //Attaches to the DashCooldown component, child of UI
    public TextMeshProUGUI omniCooldown;
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    public void SetCooldown(bool canOmni)
    {
        omniCooldown.text = "Omni Ready: " + canOmni.ToString();
    }
}