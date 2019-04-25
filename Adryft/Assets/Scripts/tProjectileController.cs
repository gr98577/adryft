using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tProjectileController : MonoBehaviour {

    private Vector3 originalPos;
    private int natCol = 1;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(life());
        originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update()
    {
        // Moves forward
        float dist = 0.03f;
        dist /= 0.02f;
        dist *= Time.deltaTime;
        
        transform.Translate(0f, dist, 0f);
	}

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        damageController dc = collision.gameObject.GetComponent<damageController>();

        //local collision counter for enemy; natCol is initialized at 1
        if (collision.tag == "Enemy")
        {
            natCol++;
        }

        // If it hits the player
        if (collision.tag == "Player")
        {
            // Does damage
           
            dc.doDamage(3, "none", originalPos, 0f);
            // Self Destructs
            Destroy(this.gameObject);
        }
        
        else if (collision.tag == "Wall" || collision.tag == "PowerBlock")
        {
            //spawn particles
            Destroy(this.gameObject);
        }

        //there's going to be a collision when the projectile is initialized; given this, each projectile
        //will have an odd-numbered number of collisions; damage will register at that point
        if(collision.gameObject.tag == "Enemy" && natCol % 2 != 0)
        {

            dc.doDamage(3, "none", originalPos, 0f);
            Destroy(this.gameObject);
//            collision.gameObject.GetComponent<turretController>().genTurret = true;

            
        }

        

    }

    // Self destructs after a given amount of time
    IEnumerator life()
    {
        yield return new WaitForSeconds(5F);
        Destroy(this.gameObject);
    }
}
