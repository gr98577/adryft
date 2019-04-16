using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrigger : MonoBehaviour {

    // Variables
    //[SerializeField]
    //private GameObject Power;
    [SerializeField]
    private GameObject ON;
    [SerializeField]
    private GameObject OFF;
    private bool powObj = false, plyr = false, enemy = false;

    // Use this for initialization
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            ON.SetActive(true);
            OFF.SetActive(false);
        }
    }
}
