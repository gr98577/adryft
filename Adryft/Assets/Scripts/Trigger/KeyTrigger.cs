using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour {

    // Variables
    //[SerializeField]
    //private GameObject Power;
    [SerializeField]
    private GameObject ON;
    [SerializeField]
    private GameObject OFF;
    private bool powObj = false, plyr = false, enemy = false;
    [SerializeField]
    private int color;
    [SerializeField]
    private GameObject sound;
    private bool open;

    // Use this for initialization
    void Start()
    {
        open = false;
        ON.SetActive(false);
        OFF.SetActive(true);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<KeyContainer>().hasKey(color) && !open)
            {
                var clone = Instantiate(sound, transform.position, Quaternion.identity);
                ON.SetActive(true);
                OFF.SetActive(false);
                open = true;
            }
            else if (!open)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
