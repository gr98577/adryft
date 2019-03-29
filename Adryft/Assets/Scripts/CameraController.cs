using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private float smoothAmount;
    private float smoothAmountActual;
    private bool shake;
    [SerializeField]
    private float shakeAmount;
    private float m_magnitude;
    private float delay;

    // Use this for initialization
    void Start () {
        shake = false;
        // Finds the player
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (Time.timeScale != 0f)
        {
            smoothAmountActual = smoothAmount * ((1 / Time.deltaTime) / 120);
            followPlayer();
        }
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

        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, smoothAmountActual);

        if (shake)
        {
            if (player.GetComponent<SpriteRenderer>().color.a != 0)
            {
                float x = Random.Range(-1f, 1f) * m_magnitude;
                float y = Random.Range(-1f, 1f) * m_magnitude;
                float z = Random.Range(-0.2f, 0.2f) * m_magnitude;

                x = x + position.x;
                y = y + position.y;
                z = z + position.z;

                position = new Vector3(x, y, z);
            }
        }

        transform.position = position;
    }

    public IEnumerator cameraShake(float duration, float magnitude)
    {
        m_magnitude = magnitude * shakeAmount;
        shake = true;
        yield return new WaitForSeconds(duration);
        shake = false;
    }
}
