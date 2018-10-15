using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    private float range = 2;
    private float dist;
    //private int health = 6;
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
        FacePlayer();

        // Send out a Raycast and stores the hit result in hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= range && hit.transform.tag == "Player")
        {
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

        //if (health <= 0)
        //{
            
        //    Destroy(this.gameObject);
        //}
    }

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

    void MoveTo() {

        // moves in a straight line directly toward the player
        transform.position = Vector3.MoveTowards(transform.position, searchLocation, mSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(3, "none", transform.position, 1f);
            Debug.Log("hello");
        }
    }
}
    
