using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public bool paused = false;
    //public float volumeSliderValue;
    //public AudioSource ambiance;
    //private GUIStyle style;
    public GameObject PauseMenu;
    //public Font Font;
    private void Start()
    {
        paused = false;

        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetButtonDown("pauseButton"))
            paused = togglePause();
        if (paused)
        {
            PauseMenu.SetActive(true);
        }

    }

    //void OnGUI()
    //{
    //    style = new GUIStyle();
    //    style.fontSize = 20;
    //    style.font = Font;
    //    if (paused)
    //    {
    //        PauseMenu.SetActive(true);
    //        GUILayout.BeginArea(new Rect(550, 400, 1000, 1000));

    //        GUI.Label(new Rect(200,0,300,90),"Game is paused!",style);
    //        GUI.Label(new Rect(200,150,300,90),"Volume",style);
    //        volumeSliderValue = GUI.HorizontalSlider(new Rect(200, 200, 300, 90), volumeSliderValue, 0.0f, 1.0f);
    //        ambiance.volume = volumeSliderValue;
    //        if (GUI.Button(new Rect(200,50,300,50),"Click me to unpause", GUI.skin.button))
    //        {
    //            paused = togglePause();
    //        }
    //        GUILayout.EndArea();
    //    }
    //}

    bool togglePause()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}
