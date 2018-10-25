using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Variables
    private GameObject player;

    // Use this for initialization
    void Start () {
        // Finds the player
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        followPlayer();
	}

    void followPlayer()
    {
        // Gets the player's position
        Vector3 playerPosition = player.transform.position;

        // Sets the Camera's position to the player's position.
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y, -4);
        transform.position = position;
    }
}
