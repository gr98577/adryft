
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        // If the player presses enter
        if (Input.GetButtonDown("Submit"))
        {
            // Restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
