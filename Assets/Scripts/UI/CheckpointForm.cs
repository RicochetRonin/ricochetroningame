using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointForm : MonoBehaviour
{

    public GameObject checkpointButton;
    public checkpoint[] checkpoints;
    public GameObject buttonParent;

    public GameManager gameManager;


    private void OnEnable()
    {
        checkpoints = GameObject.FindObjectsOfType<checkpoint>().OrderBy(go => go.transform.position.x).ToArray(); ;

        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoint cp = checkpoints[i];
            GameObject newButton = Instantiate(checkpointButton, buttonParent.transform);
            newButton.GetComponent<CheckpointButton>().buttonText.text = "CP " + (i + 1).ToString();
            newButton.GetComponent<CheckpointButton>().setCheckpoint(cp);
            newButton.GetComponent<Button>().onClick.AddListener((delegate { this.SelectCP(cp); }));
        }
    }
    public void SelectCP(checkpoint cp)
    {
        Debug.Log("Button clicked for " + cp.name);
        gameManager.ParamMenuSpawnCheckPoint(cp);
    }


}
