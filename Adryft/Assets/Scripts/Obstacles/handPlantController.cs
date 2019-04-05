using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPlantController : MonoBehaviour {

    private bool flip;
    private float time;
    private Vector3 backUp;

    private int animStage;
    private bool playerInRange;
    private SpriteRenderer sr;
    private Sprite[] up = new Sprite[5], down = new Sprite[5];
    private GameObject player;

	// Use this for initialization
	void Start () {

        sr = GetComponent<SpriteRenderer>();
        up[0] = Resources.Load<Sprite>("SlowHaz/SlowUp0");
        up[1] = Resources.Load<Sprite>("SlowHaz/SlowUp1");
        up[2] = Resources.Load<Sprite>("SlowHaz/SlowUp2");
        up[3] = Resources.Load<Sprite>("SlowHaz/SlowUp3");
        up[4] = Resources.Load<Sprite>("SlowHaz/SlowUp4");

        down[0] = Resources.Load<Sprite>("SlowHaz/SlowDown0");
        down[1] = Resources.Load<Sprite>("SlowHaz/SlowDown1");
        down[2] = Resources.Load<Sprite>("SlowHaz/SlowDown2");
        down[3] = Resources.Load<Sprite>("SlowHaz/SlowDown3");
        down[4] = Resources.Load<Sprite>("SlowHaz/SlowDown4");

        int temp = (int)Random.Range(0, 2);
        if (temp < 1)
        {
            sr.flipX = true;
        }
        animStage = 2;
        playerInRange = false;

        StartCoroutine(animCycle());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerInRange)
        {
            sr.sprite = up[animStage];
            if (player.transform.position.y < transform.position.y)
            {
                sr.sortingOrder = -1;
            }
            else
            {
                sr.sortingOrder = 1;
            }
        }
        else
        {
            sr.sprite = down[animStage];
        }
        //transform.up = Vector3.Lerp(transform.up, backUp, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }

    IEnumerator animCycle()
    {
        bool dir = true;
        while (true)
        {
            yield return new WaitForSeconds(0.075f);

            if (animStage > 4 || animStage < 0)
            {
                animStage = 2;
            }

            if (dir)
            {
                if (animStage == 4)
                {
                    animStage--;
                    dir = false;
                }
                else
                {
                    animStage++;
                }
            }
            else
            {
                if (animStage == 0)
                {
                    animStage++;
                    dir = true;
                }
                else
                {
                    animStage--;
                }
            }
        }
    }
}
