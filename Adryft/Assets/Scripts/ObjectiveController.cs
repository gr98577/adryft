using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {

    public GameObject player;
    public GameObject success;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("CONGRADULATIONS");
            var clone = Instantiate(success, player.transform.position, Quaternion.identity);

            Destroy(player);
        }
    }
}
