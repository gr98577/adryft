using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour {

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
    private AudioSource oofSource;
    private Rigidbody2D rb2d;
    private bool kb = false;

    // getter function for health
    public int getHealth()
    { return health; }

    // getter function for max health
    public int getMaxHealth()
    { return MAX_HEALTH; }

    // Use this for initialization
    void Start () {
        health = MAX_HEALTH;
        oofSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            try
            {
                var clone = Instantiate(deathSpawn, transform.position, Quaternion.identity);
            }
            catch
            {
                Debug.Log("no death spawn");
            }
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
            Destroy(this.gameObject);
        }
    }

    public void giveHealth(int amount)
    {
        health += amount;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }

    public void doDamage(int amount, string type, Vector3 location, float kbAmount)
    {
        health -= amount;
        oofSource.Play();

        if (isMobile)
        {
            if (!kb && kbAmount != 0)
            {
                StartCoroutine(knockback(location, kbAmount));
            }
        }
        else
        {
            this.SendMessage("hit", 0.5f);
        }

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
        else if (type == "")
        {

        }
    }

    IEnumerator knockback(Vector3 location, float mult)
    {
        kb = true;

        Vector2 rot = new Vector2(
                    transform.position.x - location.x,
                    transform.position.y - location.y);

        rot *= 50f * mult;

        rb2d.AddForce(rot * 8f);
        yield return new WaitForSeconds(0.05F * mult);
        rb2d.AddForce(rot * 3f);
        yield return new WaitForSeconds(0.025F * mult);
        rb2d.AddForce(rot * 1f);
        yield return new WaitForSeconds(0.025F * mult);
        rb2d.velocity = new Vector2(0,0);

        kb = false;
    }

    IEnumerator fireDamage()
    {
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(.5F);
            health -= 1;
            oofSource.Play();
        }
    }
}
