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
        // 1. Need to grab current input action? And always display
        // 2. Replace True and False with the sprite
        // omniCooldown.text = "Omni Ready: "+canOmni.ToString();
        // Debug.Log(_playerControls.Abilities.OmniReflect.bindings.ToString());
        // string curDeviceBtn = _playerControls.Abilities.OmniReflect.bindings;
        // Debug.Log(curDeviceBtn);
        omniCooldown.text = "           " + canOmni.ToString();
    }
}