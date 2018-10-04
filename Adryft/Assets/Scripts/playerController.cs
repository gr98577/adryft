using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    // Game Object Variables
    [SerializeField]
    private AudioSource ochSource;
    [SerializeField]
    private GameObject gameOver;

   
    

    //variables
    private float mSpeed;
    private float tSpeed;
    private static int STARTING_PLAYER_HEALTH = 20;
    private static int health;
    private bool stun;
    private int dash;
    private Vector3 direction;

    //get/set for health for the healthbar
    public static int playerHealth
    {
        get { return health; }
        set { health -= value; }
    }

    public static int startingPlayerHealth
    {
        get { return STARTING_PLAYER_HEALTH; }
    }



    // Use this for initialization
    void Start()
    {
        mSpeed = 2f;
        health = STARTING_PLAYER_HEALTH;
        Debug.Log("GAME START!");

        ochSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stun)
        {
            //do nothing for now
        }
        else
        {
            Move();
        }

        if (health <= 0)
        {
            var clone = Instantiate(gameOver, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        if (Input.GetButtonDown("Jump") && dash == 0)
        {
            dash = 1;
            StartCoroutine(Dash());
        }

        if (dash == 1)
        {
            tSpeed = mSpeed * Time.deltaTime * 5;
        }
        else
        {
            tSpeed = mSpeed * Time.deltaTime;
        }
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        transform.Translate(tSpeed * direction.normalized);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyProjectile"))
        {
            if (dash != 1)
            {
                //health
                health = health - 2;
                //hurt sound
                ochSource.Play();
            }
        }
    }



    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.15F);
        dash = 2;
        yield return new WaitForSeconds(1F);
        dash = 0;
    }
}

        
