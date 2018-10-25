using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private Transform Canvas;

    private playerController pc;

    private bool pickedUp = false;
    private bool canFire = false;
    private int ammunition;

    private bool stunned;
    private bool active;

    private GameObject HUD;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        ammunition = pc.getAmmunition();
    }

    // Gets if the gun is currently equipped
    public bool getEquipped()
    {
        if (pickedUp && active) return true;
        else return false;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (pickedUp)
        {
            if (active && !stunned)
            {
                // Gets the position of the mouse
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                // Attaches to the player
                attachPlayer(mousePosition);
                // Faces the mouse
                faceMouse(mousePosition);

                // Gets if the player is stunned
                stunned = pc.getIsStunned();

                // Gets the amount of ammunition the player has
                ammunition = pc.getAmmunition();
                // If the player fires the gun
                if (Input.GetButtonDown("Fire2") && canFire && ammunition > 0)
                {
                    StartCoroutine(Fire());
                }
            }
            else
            {
                //make dissapear
            }
        }
    }

    // Attaches itself to the player
    void attachPlayer(Vector3 mouse)
    {
        // Gets the player's poition in the world
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0,0,0);

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

    // faces the mouse
    void faceMouse(Vector3 mouse)
    {
        // Faces the mouse's position on the screen
        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            canFire = true;
            pickedUp = true;
            active = true;

            // creates the UI
            HUD = Instantiate(UI, Canvas);
        }
    }

    // Sets the gun to inactive
    public void setInactive()
    {
        active = false;
        HUD.SetActive(false);
    }
    // Sets the gun to active
    public void setActive()
    {
        active = true;
        HUD.SetActive(true);
    }

    // Fires a bullet
    IEnumerator Fire()
    {
        // Creates a projectile and rotates it correctly
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.up;

        // Spends one bullet
        pc.incAmmunition(-1);
        ammunition = pc.getAmmunition();

        // Slight Delay
        canFire = false;
        yield return new WaitForSeconds(0.1F);
        canFire = true;
    }

   
}
