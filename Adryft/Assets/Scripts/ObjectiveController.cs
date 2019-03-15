using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private GameObject success;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            var clone = Instantiate(success, player.transform.position, Quaternion.identity);

            // self destructs
            //Destroy(player);

            Time.timeScale = 0f;
        }
    }
}
