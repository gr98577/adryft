using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTrigger : MonoBehaviour {

    // Variables
    //[SerializeField]
    //private GameObject Power;
    [SerializeField]
    private GameObject ON;
    [SerializeField]
    private GameObject OFF;

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
        if (collision.CompareTag("PowerBlock") || collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            ON.SetActive(true);
            OFF.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerBlock") || collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            ON.SetActive(false);
            OFF.SetActive(true);
        }
    }
}
