using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugHeal : MonoBehaviour {

    private List<GameObject> coList;
    private List<GameObject> deList;
    [SerializeField]
    private float healFactor;
    [SerializeField]
    private GameObject gas1;
    [SerializeField]
    private GameObject gas2;
    private float secondCount;
    private Slug parent;
    private bool alliesNeedHeal;
    private int frameCount;

    private Vector3 moveLoc;

    // Use this for initialization
    void Start () {
        coList = new List<GameObject>();
        deList = new List<GameObject>();
        secondCount = 0f;
        parent = GetComponentInParent<Slug>();
        moveLoc = new Vector3(0f, 0f, 0f);
        frameCount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (coList.Count != 0)
        {
            parent.numAlliesNear = coList.Count;
            alliesNeedHeal = false;
            foreach (GameObject x in coList)
            {
                damageController xdc = x.GetComponent<damageController>();
                if(xdc.getHealth() < xdc.getMaxHealth())
                {
                    alliesNeedHeal = true;
                }
            }

            if (alliesNeedHeal)
            {
                if (!parent.wait && !parent.isHealing)
                {
                    parent.wait = true;
                    StartCoroutine(parent.Heal());
                    //GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (!parent.wait && parent.isHealing)
                {
                    parent.wait = true;
                    StartCoroutine(parent.Move());
                    //GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        else
        {
            parent.numAlliesNear = 0;
        }
        

        if (parent.isHealing)
        {
            secondCount += healFactor * Time.deltaTime;
            if (secondCount >= 1f)
            {
                secondCount -= 1f;
                foreach (GameObject x in coList)
                {
                    x.GetComponent<damageController>().giveHealth(1);
                }
            }
            if (!parent.wait)
            {
                
            }

            frameCount++;
            if (frameCount == 9)
            {
                var clone1 = Instantiate(gas1, transform.position, Quaternion.identity);
            }
            else if (frameCount >= 18)
            {
                frameCount = 0;
                var clone2 = Instantiate(gas2, transform.position, Quaternion.identity);
            }
        }
        else
        {
            moveLoc = new Vector3(0f, 0f, 0f);
            foreach (GameObject x in coList)
            {
                moveLoc += x.transform.position;
            }
            moveLoc /= coList.Count;

            if (coList.Count != 0)
            {
                parent.MoveTo(moveLoc);
            }
            else
            {
                parent.RunAway();
            }
        }
	}

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Enemy") && collision.GetComponent<damageController>().getIsMobile())
        {
            coList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetComponent<damageController>().getIsMobile())
        {
            coList.Remove(collision.gameObject);
        }
    }
}
