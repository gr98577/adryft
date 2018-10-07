using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour {

    private int health;
    [SerializeField]
    private int MAX_HEALTH;
    [SerializeField]
    private GameObject deathSpawn;
    private AudioSource oofSource;

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
            Destroy(this.gameObject);
        }
    }

    public void doDamage(int amount, string type, Vector3 location)
    {
        health -= amount;
        oofSource.Play();
        this.SendMessage("hit", 0.5f);

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
