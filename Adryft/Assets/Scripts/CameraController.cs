using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Variables
    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        followPlayer();
	}

    void followPlayer()
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 position = new Vector3(playerPosition.x, playerPosition.y, -4);
        transform.position = position;
    }
}
