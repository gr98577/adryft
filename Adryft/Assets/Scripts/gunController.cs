using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour {

    // Variables
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject projectile;

    private playerController pc;

    private bool pickedUp = false;
    private bool canFire = false;
    private int ammunition;

    private bool stunned;
    private bool active;

    private int n = 0;

    // Use this for initialization
    void Start () {
		pc = player.GetComponent<playerController>();
        ammunition = pc.getAmmunition();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (pickedUp && active)
        {
            stunned = pc.getIsStunned();
            if (!stunned)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                attachPlayer(mousePosition);
                faceMouse(mousePosition);

                ammunition = pc.getAmmunition();
                if (Input.GetButtonDown("Fire2") && canFire && ammunition > 0 )
                {
                    StartCoroutine(Fire());
                }
            }
        }
    }

    void attachPlayer(Vector3 mouse)
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0,0,0);


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

    void faceMouse(Vector3 mouse)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canFire = true;
            pickedUp = true;
            active = true;
        }
    }

    IEnumerator Fire()
    {
        n++;
        
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.up;
        pc.incAmmunition(-1);
        ammunition = pc.getAmmunition();

        Debug.Log("Shoot no." + n + " | Ammo: " + ammunition + "/20");

        canFire = false;
        yield return new WaitForSeconds(0.1F);
        canFire = true;
    }

   
}
