using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tProjectileController : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
        StartCoroutine(life());
    }
	
	// Update is called once per frame
	void Update()
    {
        // Moves forward
        transform.Translate(0f, 0.03f, 0f);
	}

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it hits the player
        if (collision.CompareTag("Player"))
        {
            // Does damage
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(3, "none", transform.position, 1f);
            // Self Destructs
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            //spawn particles
            Destroy(this.gameObject);
        }
    }

    // Self destructs after a given amount of time
    IEnumerator life()
    {
        yield return new WaitForSeconds(5F);
        Destroy(this.gameObject);
    }
}
