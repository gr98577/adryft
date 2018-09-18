using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tProjectileController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0f, 0.03f, 0f);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
