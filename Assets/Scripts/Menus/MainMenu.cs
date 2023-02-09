using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsSelect; //Whatever should be highlighted when going to the settings menu
    public GameObject mainMenuSelect; //Whatever should be highlighted when going to the main menu

    public void NewGame ()
    {
        SceneManager.LoadScene("Cutscene1"); //Can do name, build index, etc...
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void SelectSettingsDefault()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsSelect);
    }

    public void SelectMainMenuDefault()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuSelect);
    }

   
}
