using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject next;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<Pointer>().SetCheckpoint(gameObject);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            next.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
