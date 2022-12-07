using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SaveWarningText;
    public bool WarningAcknowledged;
    private void Start()
    {
        WarningAcknowledged = false;
        Time.timeScale = 1f;
    }
    public void NewGame()
    {
        PlayerData data = SaveSystem.loadPlayer();
        if(data.level != 1)
        {
            StartCoroutine(SaveWarning());
            if (WarningAcknowledged)
            {
                data.level = 1;
                //we have to add our scenes to the buildsettings for this to work
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            data.level = 1;
            //we have to add our scenes to the buildsettings for this to work
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        
    }
    public void LoadGame()
    {
        PlayerData data = SaveSystem.loadPlayer();
        //we have to add our scenes to the buildsettings for this to work
        SceneManager.LoadScene(data.level);
        Debug.Log(data.level);

    }
    IEnumerator SaveWarning()
    {
        SaveWarningText.SetActive(true);
        yield return new WaitForSeconds(5);
        SaveWarningText.SetActive(false);
    }
    public void SaveWarningButton()
    {
        WarningAcknowledged = true;
        NewGame();
    }
    public void QuitGame()
    {
        Debug.Log("quit the game!(as an application)");
        Application.Quit();
    }
}
