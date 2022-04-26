using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        PlayerData data = SaveSystem.loadPlayer();
        data.level = 0;
        //we have to add our scenes to the buildsettings for this to work
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame()
    {
        PlayerData data = SaveSystem.loadPlayer();
        //we have to add our scenes to the buildsettings for this to work
        SceneManager.LoadScene(data.level);
        Debug.Log(data.level);
    }
    public void QuitGame()
    {
        Debug.Log("quit the game!(as an application)");
        Application.Quit();
    }
}
