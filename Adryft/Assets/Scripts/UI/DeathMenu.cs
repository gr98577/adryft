using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public static bool gameIsPaused;
    [SerializeField]
    public GameObject menuUI;
    private playerController pc;


    // Use this for initialization
    void Start()
    {
        //pauseResume();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    public void pauseResume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void pauseRestart()
    {
        pauseResume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void pauseQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
