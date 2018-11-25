﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour {

    // Variables
    private int health;
    [SerializeField]
    private int MAX_HEALTH;
    [SerializeField]
    private GameObject deathSpawn;
    [SerializeField]
    private GameObject deathDrop;
    [SerializeField]
    private bool isMobile;
    [SerializeField]
    private float dropPercent;
    [SerializeField]
    private bool doesFly;
    [SerializeField]
    private bool player;
    private AudioSource oofSource;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private bool kb = false;
    private bool fell = false;

    // getter function for health
    public int getHealth()
    { return health; }

    // getter function for max health
    public int getMaxHealth()
    { return MAX_HEALTH; }

    // Setter function for isFlying
    public void setFlying(bool dash)
    { doesFly = dash; }
    // Getter function for isFlying
    public bool getFlying()
    { return doesFly; }

    // Use this for initialization
    void Start () {
        // Initialization
        health = MAX_HEALTH;
        oofSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            // If the owner is the player
            if (player)
            {
                // Tries to spawn the deathSpawn sprite
                try
                {
                    var clone = Instantiate(deathSpawn, transform.position, Quaternion.identity);
                }
                catch
                {
                    Debug.Log("no death spawn");
                }
            }
            // If the owner didn't fall
            else if (!fell)
            {
                // Tries to spawn the deathSpawn sprite
                try
                {
                    var clone = Instantiate(deathSpawn, transform.position, Quaternion.identity);
                }
                catch
                {
                    Debug.Log("no death spawn");
                }

                // Tries to have a random change of dropping a pickup
                try
                {
                    float rand = Random.Range(0.0f, 1.0f);
                    if (rand < dropPercent)
                    {
                        var drop = Instantiate(deathDrop, transform.position, Quaternion.identity);
                    }
                }
                catch
                {
                    Debug.Log("no death drop");
                }
            }
            // Self destructs
            Destroy(this.gameObject);
        }
    }

    // Gives a certain amount of health
    public void giveHealth(int amount)
    {
        health += amount;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }

    // Main component of damage controller
    public void doDamage(int amount, string type, Vector3 location, float kbAmount)
    {
        // Does damage
        health -= amount;
        oofSource.Play();

        // Turns the sprite red for a breif moment
        StartCoroutine(redFlash());

        // If its not a stationary enemy
        if (isMobile)
        {
            // Attempts to knock back the owner slightly
            if (!kb && kbAmount != 0)
            {
                StartCoroutine(knockback(location, kbAmount));
            }
        }
        else
        {
            // Stuns the turret for a breif second
            SendMessage("hit", 0.5f);
        }

        // Does different effecs based off of the type of damage delt
        if (type == "fire")
        {
            StartCoroutine(fireDamage());
        }
        else if (type == "stun")
        {
            this.SendMessage("stun", 2f);
        }
        else if (type == "push")
        {
            // to implement
        }
        else if (type == "fall")
        {
            // Only does this damage if the owner can't fly
            if (!doesFly)
            {
                this.SendMessage("stun", 2f);
                StartCoroutine(fall());
            }
        }

        ///*
        if (player)
        {
            playerController plyr = GetComponent<playerController>();
            float duration = kbAmount / 20;
            StartCoroutine(plyr.mainCamera.cameraShake(0.1f, duration));
        }
        //*/
    }

    public void HealthLvlUp(int ammount)
    {
        MAX_HEALTH += ammount;
        health = MAX_HEALTH;
    }

    // Fall Damage
    IEnumerator fall()
    {
        // Shrinks the owner to simulate falling
        for (float i = 1f; i >= 0f; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.0025f);
            transform.localScale = new Vector3(i, i, i);
        }
        // Sets fell to true and health to zero
        fell = true;
        health = 0;
    }

    // Knockback
    IEnumerator knockback(Vector3 location, float mult)
    {
        kb = true;

        // Calculates the direction of the force.
        Vector2 rot = new Vector2(
                    transform.position.x - location.x,
                    transform.position.y - location.y);
        rot *= 50f * mult;

        // Applies knockback
        rb2d.AddForce(rot * 8f);
        yield return new WaitForSeconds(0.05F * mult);
        rb2d.AddForce(rot * 3f);
        yield return new WaitForSeconds(0.025F * mult);
        rb2d.AddForce(rot * 1f);
        yield return new WaitForSeconds(0.025F * mult);
        // Sets all velocity to zero
        rb2d.velocity = new Vector2(0,0);

        kb = false;
    }

    // Fire Damage
    IEnumerator fireDamage()
    {
        // does continuous damage over time
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(.5F);
            health -= 1;
            oofSource.Play();
        }
    }

    // Turns the sprite red for a breif moment
    IEnumerator redFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1F);
        sr.color = Color.white;
    }

    public void SaveTo(PlayerData pd)
    {
        pd.d_healh = health;
        pd.d_maxHealth = MAX_HEALTH;
    }

    public void LoadFrom(PlayerData pd)
    {
        health = pd.d_healh;
        MAX_HEALTH = pd.d_maxHealth;
    }
}