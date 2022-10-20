using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grabAnimationTrigger : MonoBehaviour
{
    public GameObject anim;
    public PlayerController player;
    // Start is called before the first frame update

    public void playAnimation()
    {
        anim.SetActive(true);
        player.GetComponent<PlayerController>().speed = 0;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
