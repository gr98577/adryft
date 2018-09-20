﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //variables
    public float mSpeed;
    public int health;
    // Use this for initialization
    void Start () {
        mSpeed = .03f;
        health = 20;
        Debug.Log("GAME START!");
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(mSpeed * Input.GetAxisRaw("Horizontal"), mSpeed * Input.GetAxisRaw("Vertical"), 0f);

        if (health <= 0)
        {
            Debug.Log("GAME OVER!");
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyProjectile"))
        {
            health = health - 2;
        }
    }
}
