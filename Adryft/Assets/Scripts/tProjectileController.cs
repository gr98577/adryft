using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tProjectileController : MonoBehaviour {

    private Vector3 originalPos;

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
        // If it hits the player
        if (collision.tag == "Player")
        {
            // Does damage
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(3, "none", originalPos, 0f);
            // Self Destructs
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Wall" || collision.tag == "PowerBlock")
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
