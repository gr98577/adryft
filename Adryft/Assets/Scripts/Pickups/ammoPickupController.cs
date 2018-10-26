using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickupController : MonoBehaviour {

    // Variables
    [SerializeField]
    private int amount;
    private bool grow;
    private float maxSize;
    private float minSize;
    private float size;

    // Use this for initialization
    void Start () {
        // These are used for the sprite growing and shrinking
        maxSize = 1.3f;
        minSize = 1.0f;
        size = minSize;
        grow = true;
	}

    // Update is called once per frame
    void Update()
    {
        // Grows until it reaches max size, then shrinks
        if (grow)
        {
            size += 0.002f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size >= maxSize) grow = false;
        }
        // Shrinks until it reaches min sze then grows
        else
        {
            size -= 0.002f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size <= minSize) grow = true;
        }
    }

    // Called on colission
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collided with the player give the player ammo and destroys itself
        if (collision.CompareTag("Player"))
        {
            // Get the player script
            playerController pc = collision.gameObject.GetComponent<playerController>();
            // If the player isn't at max ammo
            if (pc.getAmmunition() != pc.getMaxAmmo())
            {
                // Gives the player ammo and then self destructs
                pc.incAmmunition(amount);
                Destroy(this.gameObject);
            }
        }
    }



}
