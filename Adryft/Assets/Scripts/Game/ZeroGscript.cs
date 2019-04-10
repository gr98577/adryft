using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGscript : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject notif;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        notif.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collided with the player
        if (collision.gameObject == player)
        {
            Debug.Log("ZeroG: ON");
            player.GetComponent<playerController>().zeroG = true;
            player.GetComponent<playerAnimScript>().zeroG = true;
            notif.SetActive(true);
            //player.GetComponent<TrailRenderer>().enabled = true;
            //player.GetComponent<TrailRenderer>().time = 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If it collided with the player
        if (collision.gameObject == player)
        {
            Debug.Log("ZeroG: OFF");
            player.GetComponent<playerController>().zeroG = false;
            player.GetComponent<playerAnimScript>().zeroG = false;
            notif.SetActive(false);
            //player.GetComponent<TrailRenderer>().enabled = false;
            //player.GetComponent<TrailRenderer>().time = 0.075f;

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }
}
