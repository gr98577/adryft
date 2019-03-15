using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    private bool isInUse;
    private SpriteRenderer sr;
    private Sprite[] es;
    private int i;
    private GameObject player;
    private float range = 0.5f;
    private float dist;
    private float xDisplacement;
    private float yDisplacement;
    private bool isHeld;

    public void setUse(bool isUsing)
    {
        isInUse = isUsing;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        sr = GetComponent<SpriteRenderer>();

        es = new Sprite[6];
        es[0] = Resources.Load<Sprite>("ElectroBox/B0");
        es[1] = Resources.Load<Sprite>("ElectroBox/B1");
        es[2] = Resources.Load<Sprite>("ElectroBox/B2");
        es[3] = Resources.Load<Sprite>("ElectroBox/B3");
        es[4] = Resources.Load<Sprite>("ElectroBox/B4");
        es[5] = Resources.Load<Sprite>("ElectroBox/B5");
        sr.sprite = es[0];

        i = 0;

        StartCoroutine(Shock());
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (Input.GetButtonDown("Fire3") && dist <= range)
        {
            isHeld = true;

            xDisplacement = transform.position.x - player.transform.position.x;
            yDisplacement = transform.position.y - player.transform.position.y;
        }

        if (isHeld && Input.GetButton("Fire3"))
        {
            FollowPlayer();
        }

        if (Input.GetButtonUp("Fire3") || Input.GetButtonDown("Jump") || dist > range)
        {
            isHeld = false;
        }
    }

    // Follows the player
    void FollowPlayer()
    {
        float x = player.transform.position.x + xDisplacement;
        float y = player.transform.position.y + yDisplacement;

        Vector3 position = new Vector3(0, 0, 0);

        //Sets its location to the player's
        position = new Vector3(x, y, transform.position.z);
        transform.position = position;
    }

    IEnumerator Shock()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (isInUse)
            {
                sr.sprite = es[0];
            }
            else
            {
                i++;
                if (i >= 6) i = 1;
                sr.sprite = es[i];
            }
        }
    }
}