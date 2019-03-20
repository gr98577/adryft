using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class turretController : MonoBehaviour
{

    // Variables

    private GameObject player;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private LayerMask mask;

    private float range = 5;
    private float dist;
    private bool canShoot = true;
    private bool stunned;
    private bool isHit;
    private bool search = false; //true when the PC is out of aggro range after having been in it
    private SpriteRenderer currSprite; //retrieves reference to current SpriteRenderer of gameObject; can get
    //sprite of gameObject here to change accordingly
    private enum DIR {NONE, UP, UPRHT, RHT, DWNRHT, DWN, DWNLFT, LFT, UPLFT} //enumeration of directions to pass to other functions
    private DIR sprDir;
    private bool batMode, sprDone;
    Sprite[] idleSprites;
    System.Random rand = new System.Random();
    bool hivar;
    [SerializeField]
    private DIR[] noTurn = new DIR[8];


    // Use this for initialization
    void Start()
    {
        
        idleSprites = new Sprite[8];
        idleSprites[0] = Resources.Load<Sprite>("turSprites/up/idle");
        idleSprites[1] = Resources.Load<Sprite>("turSprites/up_right/idle");
        idleSprites[2] = Resources.Load<Sprite>("turSprites/right/idle");
        idleSprites[3] = Resources.Load<Sprite>("turSprites/down_right/idle");
        idleSprites[4] = Resources.Load<Sprite>("turSprites/down/idle");
        idleSprites[5] = Resources.Load<Sprite>("turSprites/down_left/idle");
        idleSprites[6] = Resources.Load<Sprite>("turSprites/left/idle");
        idleSprites[7] = Resources.Load<Sprite>("turSprites/up_left/idle");

        hivar = false;

        batMode = sprDone = false;
        currSprite = gameObject.GetComponent<SpriteRenderer>();
        currSprite.sprite = idleSprites[2];
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned || isHit)
        {

        }
        else
        {

            //FacePlayer();
            dir = FacePlayer(); //Vector3 leading to the player position is received here
            //the transform.up_left of the turret is changed iff there's a clear shot (so it doesn't
            //face the player through walls)

            // Send out a Raycast and stores the hit result in hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range*2, mask.value);
            dist = Vector3.Distance(player.transform.position, transform.position);


            //seen = (hit.transform.tag == "Player");
            Vector3 shootDir;

            shootDir = dir; //collects dir to be passed to EscalateToFire to determine what orientation of sprites to load
                            //this variable will have their x and y components rounded to the nearest integer after given some 
                            //"slack" in an attempt to make turrets more likely to turn to their proper directions when shooting

            // If it can see the player and the player is withing range
            if (canShoot && hit.transform.tag == "Player" && dist <= range)
            {
                StopCoroutine(idleSwing(tag));


               

                //rounds x and y components of shootDir
                //value to be rounded divided by 0.1 to add slack (seems to work decently...)
                shootDir.x = Mathf.Round(dir.x / 0.099f);
                shootDir.y = Mathf.Round(dir.y / 0.099f);

                //Debug.Log(shootDir);

                StartCoroutine(EscalateToFire(shootDir));  //(loads in semi-quick succession the 
                                                           //seen_start/end and fire_start/end sprites)

                
                
            }
            else
            {
                StopCoroutine(EscalateToFire(shootDir));
                StartCoroutine(idleSwing(hit.transform.tag));
                
            }


        }
    }


    //switches current sprite to another idle orientation logically and randomly
    IEnumerator idleSwing(string tag)
    {
        

        if (tag != "Player")
        {
            

            if (currSprite.sprite == idleSprites[0])
            {
                //(11.8) random number generator with fairly large range is used to slow down sprite changes
                //(merely using the range of relevant numbers makes the turrets spaz out)
                //(11.10) will only turn to directions it's not configured to NOT turn to (denoted in 
                //noTurn array in the editor
                switch (rand.Next(0, 60)){
                    case 0:

                        if (System.Array.IndexOf(noTurn, DIR.UPLFT) == -1)
                        {
                            currSprite.sprite = idleSprites[7];
                        }
                        break;
                    case 1:
                        if (System.Array.IndexOf(noTurn, DIR.UPRHT) == -1)
                        {
                            currSprite.sprite = idleSprites[1];
                        }
                        break;
                }

            }
            else if (currSprite.sprite == idleSprites[1])
            {
                switch (rand.Next(0, 60))
                {
                    case 0:
                        if (System.Array.IndexOf(noTurn, DIR.UP) == -1)
                        {
                            currSprite.sprite = idleSprites[0];
                        }
                        break;
                   
                    case 2:
                        if (System.Array.IndexOf(noTurn, DIR.RHT) == -1)
                        {
                            currSprite.sprite = idleSprites[2];
                        }
                        break;
                }

            }
            else if (currSprite.sprite == idleSprites[2])
            {
                switch (rand.Next(1, 60))
                {
                    case 1:
                        if (System.Array.IndexOf(noTurn, DIR.UPRHT) == -1)
                        {
                            currSprite.sprite = idleSprites[1];
                        }
                        break;
                    
                    case 3:
                        if (System.Array.IndexOf(noTurn, DIR.DWNRHT) == -1)
                        {
                            currSprite.sprite = idleSprites[3];
                        }
                        break;
                }


            }
            else if (currSprite.sprite == idleSprites[3])
            {
                switch (rand.Next(2, 60))
                {
                    case 2:
                        if (System.Array.IndexOf(noTurn, DIR.RHT) == -1)
                        {
                            currSprite.sprite = idleSprites[2];
                        }
                        break;
                   
                    case 4:
                        if (System.Array.IndexOf(noTurn, DIR.DWN) == -1)
                        {
                            currSprite.sprite = idleSprites[4];
                        }
                        break;
                }


            }
            else if (currSprite.sprite == idleSprites[4])
            {

                switch (rand.Next(3, 60))
                {

                    case 3:
                        if (System.Array.IndexOf(noTurn, DIR.DWNRHT) == -1)
                        {
                            currSprite.sprite = idleSprites[3];
                        }
                        break;
                  
                    case 5:
                        if (System.Array.IndexOf(noTurn, DIR.DWNLFT) == -1)
                        { 
                            currSprite.sprite = idleSprites[5];
                        }
                        break;
                }


            }
            else if (currSprite.sprite == idleSprites[5])
            {
                switch (rand.Next(4, 60))
                {

                    case 4:

                        if (System.Array.IndexOf(noTurn, DIR.DWN) == -1)
                        {
                            currSprite.sprite = idleSprites[4];
                        }
                        break;
                   
                    case 6:
                        if (System.Array.IndexOf(noTurn, DIR.LFT) == -1)
                        {
                            currSprite.sprite = idleSprites[6];
                        }
                        break;
                        

                }

            }
            else if (currSprite.sprite == idleSprites[6])
            {
                switch (rand.Next(5, 60))
                {
                    case 5:
                        if (System.Array.IndexOf(noTurn, DIR.DWNLFT) == -1)
                        {
                            currSprite.sprite = idleSprites[5];
                        }
                        break;
                   
                    case 7:
                        if (System.Array.IndexOf(noTurn, DIR.UPLFT) == -1)
                        {
                            currSprite.sprite = idleSprites[7];
                        }
                        break;


                }


            }
            else if (currSprite.sprite == idleSprites[7])
            {
                switch (rand.Next(6, 60))
                {
                    case 6:
                        currSprite.sprite = idleSprites[6];
                        break;
                    case 7:
                        currSprite.sprite = idleSprites[1];
                        break;

                }
            }

        }

        yield break;   

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;

    }

    IEnumerator EscalateToFire(Vector3 shtDir)
    {


        //if this gets put after the conditionals, a steady stream of multiple projectiles is launched
        StartCoroutine(Shoot(shtDir));



        //deduces proper orientation based on signs of x and y coordinates in relation to each other
        //essentially deduces what quadrant shtDir lies in (or if it lies on an axis)
        {
            if (shtDir.x == 0f && shtDir.y == 0f)
            {
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/fire_start");
                sprDir = DIR.RHT;

            }
            else if (shtDir.x > 0f && shtDir.y > 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/fire_start");
                sprDir = DIR.UPRHT;

            }

            else if (shtDir.x == 0f && shtDir.y > 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/fire_start");
                sprDir = DIR.UP;
            } //esclating sprites for UP
            else if (shtDir.x < 0f && shtDir.y > 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/fire_start");
                sprDir = DIR.UPLFT;
            } //...UP_LEFT

            else if (shtDir.x < 0f && shtDir.y == 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/fire_start");
                sprDir = DIR.LFT;
            } //...LEFT
            else if (shtDir.x < 0f && shtDir.y < 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/fire_start");
                sprDir = DIR.DWNLFT;
            } //...DOWN_LEFT

            else if (shtDir.x == 0f && shtDir.y < 0f)
            {

                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/fire_start");
                sprDir = DIR.DWN;
            } //...DOWN
            else if (shtDir.x > 0f && shtDir.y < 0f)
            {
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/fire_start");
                sprDir = DIR.DWNRHT;
            } //...DOWN_RIGHT

            else if (shtDir.x > 0f && shtDir.y == 0f)
            {
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/fire_start");
                sprDir = DIR.RHT;
            } //...UP_LEFT

        }


    }


/*    IEnumerator DeEscalator(DIR sprDir)
    {

        switch (sprDir)
        {
            case DIR.RHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/right/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.UPRHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_right/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.UPLFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.DWN:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.LFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.DWNLFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.UP:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/idle");
                yield return new WaitForSeconds(0.1f);
                break;

            case DIR.DWNRHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/fire_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/seen_end");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/seen_start");
                yield return new WaitForSeconds(0.1f);
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_right/idle");
                yield return new WaitForSeconds(0.1f);
                break;
        }

    }*/

    // faces the player
    //(10.28: FacePlayer() now returns a Vector3 that leads to a player position rather than constantly updating the turret's
    //transform.up_left) 
    Vector3 FacePlayer()
    {
        // Finds where the player is
        Vector3 playerPosition = player.transform.position;

        // Calculates the angle to look at the player
        //(essentially calculates sloped line)
        Vector2 direction = new Vector2(
                    playerPosition.x - transform.position.x,
                    playerPosition.y - transform.position.y);

        //returns position of player relative to the turret
        return direction;

    }

    // Shoots
    IEnumerator Shoot(Vector3 shoot)
    {
        //loads appropriate fire_end sprite
        switch (sprDir)
        {
            case DIR.UP:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up/fire_end");
                break;
            case DIR.UPRHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_up_left/fire_end");
                break;
            case DIR.UPLFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/fire_end");
                break;
            case DIR.LFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/left/fire_end");
                break;
            case DIR.DWNLFT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_left/fire_end");
                break;
            case DIR.DWNRHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down_up_left/fire_end");
                break;
            case DIR.DWN:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/down/fire_end");
                break;
            case DIR.RHT:
                currSprite.sprite = Resources.Load<Sprite>("turSprites/up_left/fire_end");
                break;

        }


        // Creates the projectile and rotates it correctly


        
        var clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.transform.up = shoot;


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
