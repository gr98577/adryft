using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickupController : MonoBehaviour {

    // private int ammunition = playerGetComponent<playerController>().getAmmunition();


    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ammunition += 5 or some other amount
            Destroy(this.gameObject);
            Debug.Log("ammunition increased by 1");
        }
    }



}
