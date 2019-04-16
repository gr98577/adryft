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
    [SerializeField]
    private GameObject bulletCasing;

    private playerController pc;

    private bool charging;

    //private bool pickedUp = false;
    private bool canFire = false;
    private int ammunition;

    private bool stunned;
    private bool active;

    private float gunCharge;

    [SerializeField]
    private GameObject HUD;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        ammunition = pc.getAmmunition();

        canFire = true;
        active = true;

        // creates the UI
        //HUD = Instantiate(UI, Canvas);
        HUD.SetActive(true);
    }

    // Gets if the gun is currently equipped
    public bool getEquipped()
    {
        if (active) return true;
        else return false;
    }
	
	// Update is called once per frame
	void LateUpdate () {
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

            if (Input.GetButtonDown("Cancel"))
            {
                gunCharge = 1f;
                charging = false;
            }

            if (canFire && ammunition > 0)
            {
                // If the player fires the gun
                if (Input.GetButtonUp("Fire2"))
                {
                    if (charging)
                    {
                        StartCoroutine(Fire());
                        gunCharge = 1f;
                        charging = false;
                    }
                }
                // Charges up bullet
                if (Input.GetButtonDown("Fire2"))
                {
                    gunCharge = 1f;
                    charging = true;
                }
                if (Input.GetButton("Fire2"))
                {
                    if (charging)
                    {
                        gunCharge += Time.deltaTime * 1.5f;
                        float gctemp = gunCharge;
                        if (gunCharge > 3f)
                        {
                            gunCharge = 3f;
                        }
                        if (gunCharge > pc.getAmmunition())
                        {
                            gunCharge = pc.getAmmunition();
                        }
                    }
                }
            }
        }

        if (charging)
        {
            //Debug.Log(gunCharge);
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
        GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.up;
        //clone.transform.localScale = clone.transform.localScale * gunCharge;
        //clone.GetComponent<pProjectileController>().setCharge(gunCharge);

        // Spends one bullet
        int price = -1 * (int)gunCharge;
        pc.incAmmunition(price);
        ammunition = pc.getAmmunition();

        float gcTemp = gunCharge;

        // Slight Delay
        canFire = false;
        if (gcTemp >= 2f)
        {
            yield return new WaitForSeconds(0.1F);
            Debug.Log("GC1: " + gunCharge);
            GameObject clone2 = Instantiate(projectile, transform.position, Quaternion.identity);
            clone2.transform.up = transform.up;
        }
        if (gcTemp >= 2.9f)
        {
            yield return new WaitForSeconds(0.1F);
            Debug.Log("GC: " + gunCharge);
            GameObject clone3 = Instantiate(projectile, transform.position, Quaternion.identity);
            clone3.transform.up = transform.up;
        }
        yield return new WaitForSeconds(0.1F);
        canFire = true;
        GameObject bullet = Instantiate(bulletCasing, transform.position, Quaternion.identity);
        bullet.transform.up = transform.right;
    }
}
