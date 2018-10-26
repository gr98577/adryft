using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour {

    // Variables

    private GameObject player;
    [SerializeField]
    private GameObject projectile;

    private float range = 5;
    private float dist;
    private bool canShoot = true;
    private bool stunned;
    private bool isHit;


    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (stunned || isHit)
        {

        }
        else
        {
            FacePlayer();

            // Send out a Raycast and stores the hit result in hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            dist = Vector3.Distance(player.transform.position, transform.position);

            // If it can see the player and the player is withing range
            if (canShoot && hit.transform.tag == "Player" && dist <= range)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    // faces the player
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

    // Shoots
    IEnumerator Shoot()
    {
        // Creates the projectile and rotates it correctly
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = transform.right;

        // Shooting cooldown
        canShoot = false;
        yield return new WaitForSeconds(0.75F);
        canShoot = true;
    }

    // Stun
    IEnumerator stun(float time)
    {
        if (!stunned)
        {
            stunned = true;
            yield return new WaitForSeconds(time);
            stunned = false;
        }
    }

    // Hit (short stun instead of knockback)
    IEnumerator hit(float time)
    {
        if (!isHit)
        {
            isHit = true;
            yield return new WaitForSeconds(time);
            isHit = false;
        }
    }
}
