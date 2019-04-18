using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MonoBehaviour {

    private damageController dc;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject player;
    public bool isHealing;
    public bool zeroG;
    private int moving = 0;
    private float mSpeed;
    public int numAlliesNear;
    public bool wait;

    private Sprite[] move;
    private Sprite[] heal;
    private Sprite[] trans;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        dc = GetComponent<damageController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dc.immune = true;
        mSpeed = 0.3f;

        move = new Sprite[4];
        move[0] = Resources.Load<Sprite>("slug/Move/1");
        move[1] = Resources.Load<Sprite>("slug/Move/2");
        move[2] = Resources.Load<Sprite>("slug/Move/3");
        move[3] = Resources.Load<Sprite>("slug/Move/4");

        heal = new Sprite[4];
        heal[0] = Resources.Load<Sprite>("slug/Heal/1");
        heal[1] = Resources.Load<Sprite>("slug/Heal/2");
        heal[2] = Resources.Load<Sprite>("slug/Heal/3");
        heal[3] = Resources.Load<Sprite>("slug/Heal/4");

        trans = new Sprite[4];
        trans[0] = Resources.Load<Sprite>("slug/Trans/1");
        trans[1] = Resources.Load<Sprite>("slug/Trans/2");
        trans[2] = Resources.Load<Sprite>("slug/Trans/3");
        trans[3] = Resources.Load<Sprite>("slug/Trans/4");

        StartCoroutine(Walk());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isHealing && numAlliesNear != 0)
        {
            sr.color = Color.grey;
            dc.immune = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
        }
        else if (numAlliesNear != 0)
        {
            sr.color = Color.white;
            dc.immune = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rb.freezeRotation = true;
        }
        else
        {
            sr.color = Color.white;
            dc.immune = false;
        }
	}

    // Moves in a straight line to the player or the player's last known location
    public void MoveTo(Vector3 searchLocation)
    {
        float x1 = transform.position.x;
        float x2 = searchLocation.x;
        float y1 = transform.position.y;
        float y2 = searchLocation.y;
        if (x1 != x2 || y1 != y2)
        {
            if (x1 > x2)
            {
                sr.flipX = false;
            }
            else if (x1 < x2)
            {
                sr.flipX = true;
            }
        }

        // moves in a straight line directly toward the player
        if (zeroG)
        {
            Vector2 direction = new Vector2(x2, x1);
            rb.AddForce(direction.normalized);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, searchLocation, mSpeed * Time.deltaTime);
        }
    }

    // Moves in a straight line to the player or the player's last known location
    public void RunAway()
    {
        float x1 = transform.position.x;
        float x2 = player.transform.position.x;
        float y1 = transform.position.y;
        float y2 = player.transform.position.y;
        if (x1 != x2 || y1 != y2)
        {
            if (x1 > x2)
            {
                sr.flipX = true;
            }
            else if (x1 < x2)
            {
                sr.flipX = false;
            }
        }

        // moves in a straight line directly toward the player
        if (zeroG)
        {
            Vector2 direction = new Vector2(x2, x1);
            rb.AddForce(-direction.normalized);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -mSpeed * Time.deltaTime);
        }
    }

    public IEnumerator Heal()
    {
        isHealing = true;
        sr.sprite = trans[0];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[1];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[2];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[3];
        yield return new WaitForSeconds(0.25f);
        wait = false;
    }

    public IEnumerator Move()
    {
        sr.sprite = trans[3];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[2];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[1];
        yield return new WaitForSeconds(0.25f);
        sr.sprite = trans[0];
        yield return new WaitForSeconds(0.25f);
        isHealing = false;
        wait = false;
    }

    private IEnumerator Walk()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (wait)
            {

            }
            else if (isHealing)
            {
                sr.sprite = heal[i];
            }
            else
            {
                sr.sprite = move[i];
            }

            i++;
            if (i >= 4)
            {
                i = 0;
            }
        }
    }
}
