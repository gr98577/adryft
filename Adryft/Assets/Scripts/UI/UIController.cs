
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIController : MonoBehaviour
{
    private GameObject audioSource;
    [SerializeField]
    private bool which;
    private AudioSource aSource;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("music");
        aSource = audioSource.GetComponent<AudioSource>();

        if (which)
        {
            aSource.mute = true;
        }
        else
        {
            aSource.pitch = 3.0f;
        }
    }

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
