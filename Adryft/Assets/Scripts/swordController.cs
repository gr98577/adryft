using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private int swingArc;
    private AudioSource swing;
    private float swingTime;

    //private bool pickedUp = false;
    private bool canSwing = false;

    private bool stunned;
    private bool active;

    private bool fullPower;

    private playerController pc;
    

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        swingTime = 0.01f;
        pc = player.GetComponent<playerController>();
        swing = GetComponent<AudioSource>();

        canSwing = true;
        active = true;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (active)
        {
            stunned = pc.getIsStunned();
            if (!stunned)
            {
                // Gets the mouse location on the screen
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                // Attaches to the player
                attachPlayer(mousePosition);

                // faces the mouse if it can swing
                if (canSwing == true)
                {
                    faceMouse(mousePosition);
                }

                // Swings
                if (Input.GetButtonDown("Fire1") && canSwing)
                {
                    // Does full damage and speed if there is enough stamina
                    if (pc.useStamina(0.15f))
                    {
                        swing.Play();
                        fullPower = true;
                        swingTime = 0.01f;
                        StartCoroutine(Swing());
                    }
                    // If there isnt enough stamina swings slower and weaker
                    else
                    {
                        fullPower = false;
                        pc.zeroStamina();
                        swingTime = 0.02f;
                        StartCoroutine(Swing());
                    }
                }
            }
        }
    }

    // Attaches to player
    void attachPlayer(Vector3 mouse)
    {
        // Gets player location
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0, 0, 0);

        // Sets its location to the player's and put itself on top of or under the player depending on mouse location
        if (mouse.y >= transform.position.y)
        {
            position = new Vector3(playerPosition.x, playerPosition.y, 1);
        }
        else
        {
            position = new Vector3(playerPosition.x, playerPosition.y, -1);
        }
        transform.position = position;
    }

    // Faces the mouse
    void faceMouse(Vector3 mouse)
    {
        // faces the mouse
        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the enemy
        if (collision.CompareTag("Enemy") && canSwing == false)
        {
            // Does full damage
            if (fullPower)
            {
                damageController dc = collision.gameObject.GetComponent<damageController>();
                dc.doDamage(4, "none", player.transform.position, 0.5f);
            }
            // Does decreased damage
            else
            {
                damageController dc = collision.gameObject.GetComponent<damageController>();
                dc.doDamage(1, "none", player.transform.position, 0f);
            }
        }
    }

    IEnumerator Swing()
    {
        // Rotates the swing arc
        canSwing = false;
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - (8 * swingArc));
        yield return new WaitForSeconds(swingTime);
        for (int i = 0; i < 16; i++)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + swingArc);
            yield return new WaitForSeconds(swingTime);
        }
        
        // Resets th position
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - (8 * swingArc));
        canSwing = true;
    }
}
