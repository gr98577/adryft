using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variables
    private GameObject player;
    private float range = 4;
    private float dist;
    private float mSpeed;
    private float tSpeed;
    private bool canMove;
    private Vector3 direction;
    private Vector2 startLocation;
    private Vector2 searchLocation;

    // Use this for initialization
    void Start()
    {
        canMove = true;
        mSpeed = .7f;
        player = GameObject.FindGameObjectWithTag("Player");
        startLocation = transform.position;
        searchLocation = startLocation;
    }

    // Update is called once per frame
    void Update()
    {
        // Faces the player
        FacePlayer();

        // Send out a Raycast and stores the hit result in hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        dist = Vector3.Distance(player.transform.position, transform.position);

        // If its looking at the player and the player is in range
        if (dist <= range && hit.transform.tag == "Player")
        {
            // Sets last known location to the player's current location
            searchLocation = player.transform.position;
            
            if (canMove)
            {
                MoveTo();
            }
        }
        else
        {
            
            if (canMove)
            {
                MoveTo();
            }
        }
    }

    // Faces the player
    void FacePlayer()
    {
        // Finds where the player is
        Vector3 playerPosition = player.transform.position;

        // Calculates the angle to look at the player
        direction = new Vector2(
                    playerPosition.x - transform.position.x,
                    playerPosition.y - transform.position.y);
        // Looks at the player
        //transform.right = direction;

    }

    // Moves in a straight line to the player or the player's last known location
    void MoveTo()
    {
        // moves in a straight line directly toward the player
        transform.position = Vector3.MoveTowards(transform.position, searchLocation, mSpeed * Time.deltaTime);
    }

    // Collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it collided with the player
        if (collision.gameObject == player)
        {
            // Does damage
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(3, "none", transform.position, 1.5f);
        }
    }
}
    
