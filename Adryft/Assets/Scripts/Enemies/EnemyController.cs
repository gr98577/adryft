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
    private int moving = 0;
    private bool canSeePlayer;
    public bool zeroG;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private float time;

    private SpriteRenderer sr;
    private Sprite still;
    private Sprite[] right;
    private Sprite[] left;
    private int i;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        canMove = true;
        mSpeed = .7f;
        player = GameObject.FindGameObjectWithTag("Player");
        startLocation = transform.position;
        searchLocation = startLocation;
        rb = gameObject.GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        still = sr.sprite;
        i = -1;
        right = new Sprite[10];
        right[0] = Resources.Load<Sprite>("EnemyAnim/RN1");
        right[1] = Resources.Load<Sprite>("EnemyAnim/RN2");
        right[2] = Resources.Load<Sprite>("EnemyAnim/RN3");
        right[3] = Resources.Load<Sprite>("EnemyAnim/RN4");
        right[4] = Resources.Load<Sprite>("EnemyAnim/RN5");
        right[5] = Resources.Load<Sprite>("EnemyAnim/RY1");
        right[6] = Resources.Load<Sprite>("EnemyAnim/RY2");
        right[7] = Resources.Load<Sprite>("EnemyAnim/RY3");
        right[8] = Resources.Load<Sprite>("EnemyAnim/RY4");
        right[9] = Resources.Load<Sprite>("EnemyAnim/RY5");
        left = new Sprite[10];
        left[0] = Resources.Load<Sprite>("EnemyAnim/LN1");
        left[1] = Resources.Load<Sprite>("EnemyAnim/LN2");
        left[2] = Resources.Load<Sprite>("EnemyAnim/LN3");
        left[3] = Resources.Load<Sprite>("EnemyAnim/LN4");
        left[4] = Resources.Load<Sprite>("EnemyAnim/LN5");
        left[5] = Resources.Load<Sprite>("EnemyAnim/LY1");
        left[6] = Resources.Load<Sprite>("EnemyAnim/LY2");
        left[7] = Resources.Load<Sprite>("EnemyAnim/LY3");
        left[8] = Resources.Load<Sprite>("EnemyAnim/LY4");
        left[9] = Resources.Load<Sprite>("EnemyAnim/LY5");
        
        StartCoroutine(Walk());

        canSeePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!zeroG)
        {
            rb.velocity = new Vector2(0,0);
        }

        // Faces the player
        FacePlayer();

        // Send out a Raycast and stores the hit result in hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range*2, mask.value);
        dist = Vector3.Distance(player.transform.position, transform.position);

        // If its looking at the player and the player is in range
        if (dist <= range && hit.transform.tag == "Player")
        {
            // Sets last known location to the player's current location
            searchLocation = player.transform.position;
            canSeePlayer = true;
            
            if (canMove)
            {
                MoveTo();
            }
            else
            {
                moving = 0;
            }
        }
        else
        {
            canSeePlayer = false;

            if (canMove)
            {
                MoveTo();
            }
            else
            {
                moving = 0;
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
        float x1 = transform.position.x;
        float x2 = searchLocation.x;
        float y1 = transform.position.y;
        float y2 = searchLocation.y;
        if (x1 != x2 || y1 != y2)
        {
            if (x1 > x2)
            {
                moving = -1;
            }
            else
            {
                moving = 1;
            }
        }
        else
        {
            moving = 0;
        }

        // moves in a straight line directly toward the player
        if (zeroG)
        {
            Vector2 direction = new Vector2(x2, x1);
            rb.AddForce(direction.normalized);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, searchLocation, mSpeed * Time.deltaTime);
        }
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

    IEnumerator Walk()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            if (canSeePlayer){ i -= 5; }
            i++;
            if (i >= 5){ i = 0; }
            if (canSeePlayer){ i += 5; }

            if (moving == -1)
            {
                sr.sprite = left[i];
            }
            else if (moving == 1)
            {
                sr.sprite = right[i];
            }
            else
            {
                i = -1;
                sr.sprite = still;
            }
        }
    }
}
    
