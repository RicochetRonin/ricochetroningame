using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public Image blood;
    //Attaches to the Health component, child of UI

    public void SetPlayerHealth(float  newHealth, float maxHealth)
    {
        hp.text = "Health " + newHealth.ToString() + "/" + maxHealth.ToString();
        blood.GetComponent<Image>().fillAmount = 1 - (newHealth / maxHealth);
    }
}
