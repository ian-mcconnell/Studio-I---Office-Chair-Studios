using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCard : MonoBehaviour
{
    public bool isPaused; 
    public GameObject titleCard;
    IEnumerator TitleCardPopup()
    {
 
        titleCard.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        titleCard.SetActive(false);

//        Time.timeScale = 1f;
    }
    void pause()
    {
        Time.timeScale = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
      //  pause();
 //       Time.timeScale = 0.0f;
        StartCoroutine(TitleCardPopup());
        //    Time.timeScale = 1f;
//        yield return new WaitForSeconds(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (titleCard.activeSelf  == true)
        {
        pause();
        }
        else
        {
            Time.timeScale = 1.0f;
        }


        
    }
}
