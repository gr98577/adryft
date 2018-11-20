﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private float smoothAmmount;
    private bool shake;
    private float m_magnitude;
    private float delay;

    // Use this for initialization
    void Start () {
        shake = false;
        // Finds the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /*
    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(cameraShake(0.2f, 0.1f));
        }
    }
    */

    // Update is called once per frame
    void LateUpdate () {
        followPlayer();
	}

    void followPlayer()
    {
        // Sets position to the player position with delay
        Vector3 playerPosition = player.transform.position;
        Vector3 desiredPosition = new Vector3(playerPosition.x, playerPosition.y, -4);
 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition -= desiredPosition;

        // Normalizes Mouse Position
        float mx = mousePosition.x;
        float my = mousePosition.y;
        if (mx > 3f){ mx = 3f; }
        else if (mx < -3f) { mx = -3f; }
        if (my > 3f){ my = 3f; }
        else if (my < -3f) { my = -3f; }
        mousePosition = new Vector3(mx, my, mousePosition.z);

        

        if (Input.GetButton("Fire2"))
        {
            if (delay < 0f)
            {
                mousePosition = mousePosition / 2;
            }
            else
            {
                delay -= Time.deltaTime;
                mousePosition = mousePosition / 4;
            }
        }
        else
        {
            delay = 0.1f;
            mousePosition = mousePosition / 4;
        }

        desiredPosition += mousePosition;

        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, smoothAmmount);

        if (shake)
        {
            float x = Random.Range(-1f, 1f) * m_magnitude;
            float y = Random.Range(-1f, 1f) * m_magnitude;
            float z = Random.Range(-0.2f, 0.2f) * m_magnitude;

            x = x + position.x;
            y = y + position.y;
            z = z + position.z;

            position = new Vector3(x, y, z);
        }

        transform.position = position;
    }

    public IEnumerator cameraShake(float duration, float magnitude)
    {
        m_magnitude = magnitude;
        shake = true;
        yield return new WaitForSeconds(duration);
        shake = false;
    }
}
