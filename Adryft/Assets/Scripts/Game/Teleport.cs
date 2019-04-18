using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    [SerializeField]
    private GameObject destination;
    [SerializeField]
    private GameObject box;
    private GameObject player;
    private bool isActive;

	// Use this for initialization
	void Start () {
        box.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Use1") && isActive)
        {

            teleport();
        }
    }

    private void teleport ()
    {
        GetComponent<AudioSource>().Play();
        player.transform.position = destination.transform.position;
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            isActive = true;
            box.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            isActive = false;
            box.SetActive(false);
        }
    }
}
