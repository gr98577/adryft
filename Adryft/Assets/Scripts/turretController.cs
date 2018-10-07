using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour {

    // Variables

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject projectile;

    private float range = 5;
    private float dist;
    private bool canShoot = true;
    private bool stunned;


    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (stunned)
        {

        }
        else
        {
            FacePlayer();

            // Send out a Raycast and stores the hit result in hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            dist = Vector3.Distance(player.transform.position, transform.position);

            if (canShoot && hit.transform.tag == "Player" && dist <= range)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    void FacePlayer()
    {
        // Finds where the player is
        Vector3 playerPosition = player.transform.position;

        // Calculates the angle to look at the player
        Vector2 direction = new Vector2(
                    playerPosition.x - transform.position.x,
                    playerPosition.y - transform.position.y);
        // Looks at the player
        transform.right = direction;

    }

    IEnumerator Shoot()
    {
        //n++;
        //Debug.Log("Shoot no." + n);
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.right;
        canShoot = false;
        yield return new WaitForSeconds(0.75F);
        canShoot = true;
    }

    IEnumerator stun(float time)
    {
        if (!stunned)
        {
            stunned = true;
            yield return new WaitForSeconds(time);
            stunned = false;
        }
    }
}
