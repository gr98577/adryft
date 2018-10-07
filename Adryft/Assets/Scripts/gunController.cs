using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour {

    // Variables
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject projectile;

    private bool pickedUp = false;
    private bool canFire = false;

    private bool stunned;
    private bool active;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (pickedUp && active)
        {
            stunned = player.GetComponent<playerController>().getIsStunned();
            if (!stunned)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                attachPlayer(mousePosition);
                faceMouse(mousePosition);

                if (Input.GetButtonDown("Fire2") && canFire)
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
        //n++;
        //Debug.Log("Shoot no." + n);
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.up;
        canFire = false;
        yield return new WaitForSeconds(0.1F);
        canFire = true;
    }

   
}
