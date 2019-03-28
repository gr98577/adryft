using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    private GameObject player;
    private SpriteRenderer sr;
    private bool started = false;
    private float i;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        i = sr.color.a;
        attachPlayer();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        {
            started = true;
        }

        if (i == 0)
        {
            Destroy(gameObject);
        }

        if (started)
        {
            i--;
        }

        sr.color = new Color(255, 255, 255, i);
    }

    

    private void attachPlayer()
    {
        // Gets the player's location
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0, 0, 0);

        //Sets its location to the player's
        position = new Vector3(playerPosition.x, playerPosition.y, 1);
        transform.position = position;
    }
}
