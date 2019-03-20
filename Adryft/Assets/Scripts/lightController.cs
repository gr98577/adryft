using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour {

    private GameObject player;
    private bool flashLight;
    private Vector3 start;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = true;
        start = new Vector3(-15, 5, 0);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        // Toggles the light on/off
        if (Input.GetButtonDown("light"))
        {
            if (flashLight) flashLight = false;
            else flashLight = true;
        }
        */

        // If its on attaches itself to the player
        if (flashLight)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            attachPlayer();
            faceMouse(mousePosition);
        }
        // If its off reverts to its default location
        else
        {
            transform.position = start;
        }
	}

    // Attaches to the player
    void attachPlayer()
    {
        // Gets the player's location
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0, 0, 0);

        //Sets its location to the player's
        position = new Vector3(playerPosition.x, playerPosition.y, 2);
        transform.position = position;
    }

    // Faces the mouse
    void faceMouse(Vector3 mouse)
    {
        // Faces the mouse
        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;
    }
}
