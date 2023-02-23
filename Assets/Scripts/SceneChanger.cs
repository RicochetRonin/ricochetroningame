using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Header("Stats UI")]
    [SerializeField] public TextMeshProUGUI timeText;
    [SerializeField] public TextMeshProUGUI bulletsReflectedText;
    [SerializeField] public TextMeshProUGUI enemiesKilledText;
    [SerializeField] public TextMeshProUGUI damageTakenText;
    [SerializeField] public TextMeshProUGUI playerdeathsText;
    public GameObject statsUI;

    private bool enabled;
    private bool canContinue = false;

    private void Update()
    {
        enabled = statsUI.activeSelf;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (enabled == false && canContinue == false)
            {
                statsUI.SetActive(true);
                timeText.text = "Total time: " + GameManager.timerVal;
                bulletsReflectedText.text = "Bullets Reflected: " + GameManager.bulletsReflected;
                enemiesKilledText.text = "Enemies Killed: " + GameManager.enemiesKilled;
                damageTakenText.text = "Damage Taken: " + GameManager.damageTaken;
                playerdeathsText.text = "Player Deaths: " + GameManager.playerDeaths;

                Time.timeScale = 0;
                canContinue = true;
            }
        }
    }

    public void ChangeScene()
    {
        canContinue = false;
        GameManager.bulletsReflected = 0;
        GameManager.enemiesKilled = 0;
        GameManager.damageTaken = 0;
        GameManager.playerDeaths = 0;
        GameManager.startTime = Time.time;


        Time.timeScale = 1;
        statsUI.SetActive(false);
        SceneManager.LoadScene(sceneName);
        GameManager.newSceneLoaded = true;
        GameManager.checkPointActive = false;
        Debug.Log("New scene, reset checkpoint");
    }
}
