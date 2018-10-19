using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    private bool flashLight;
    private Vector3 start;

    // Use this for initialization
    void Start () {
        flashLight = false;
        start = new Vector3(-15, 5, 0);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("light"))
        {
            if (flashLight) flashLight = false;
            else flashLight = true;
        }

        if (flashLight)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            attachPlayer();
            faceMouse(mousePosition);
        }
        else
        {
            transform.position = start;
        }
	}

    void attachPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0, 0, 0);
        position = new Vector3(playerPosition.x, playerPosition.y, 1);
        transform.position = position;
    }

    void faceMouse(Vector3 mouse)
    {
        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;
    }
}
