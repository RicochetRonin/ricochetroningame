using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI hp;
    //Attaches to the Health component, child of UI

    public void SetPlayerHealth(float health)
    {
        hp.text = "Health " + health.ToString();
    }
}
