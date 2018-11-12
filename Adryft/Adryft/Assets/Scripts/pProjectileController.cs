using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pProjectileController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(life());
    }

    // Update is called once per frame
    void Update()
    {
        // Moves forward
        transform.Translate(0f, 0.05f, 0f);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it hits an enemy
        if (collision.tag == "Enemy")
        {
            // Does damage
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(2, "none", transform.position, 0.5f);
            // Self Destructs
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Wall" || collision.tag == "PowerBlock")
        {
            //spawn particle effect
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

