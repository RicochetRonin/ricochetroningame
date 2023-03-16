using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private checkpoint checkpoint;

    public void setCheckpoint(checkpoint cp)
    {
        checkpoint = cp;
    }
}
