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

    public void setFlying(bool dash)
    {
        doesFly = dash;
        //Debug.Log(dash);
    }
    public bool getFlying()
    {
        return doesFly;
    }

    // Use this for initialization
    void Start () {
        health = MAX_HEALTH;
        oofSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            if (fell && !player)
            {
                Debug.Log("why was fell?");
            }
            else
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

        StartCoroutine(redFlash());

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
        else if (type == "fall")
        {
            if (!doesFly)
            {
                this.SendMessage("stun", 2f);
                StartCoroutine(fall());
                Debug.Log(doesFly);
            }
        }
    }

    IEnumerator fall()
    {
        for (float i = 1f; i >= 0f; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.0025f);
            transform.localScale = new Vector3(i, i, i);
        }
        fell = true;
        health = 0;
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

    IEnumerator redFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1F);
        sr.color = Color.white;
    }
}
