using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public static bool gameIsPaused;
    [SerializeField]
    public GameObject menuUI;
    [SerializeField]
    private GameObject check;

	// Use this for initialization
	void Start () {
        pauseResume();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !check.activeSelf)
        {
            if (gameIsPaused) { pauseResume(); }
            else { pauseOpen(); }
        }
	}

    private void pauseOpen()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        // Have to go in a make it manually disable scripts that continue even when timescale is 0
        // You can check if the game is paused from any in game script with " if(pauseScript.gameIsPaused) "
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
