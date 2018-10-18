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
    private bool stunned;
    private int dash;
    private int ammunition = 20;
    private Vector3 direction;

    public int getAmmunition()
    {
        return ammunition;
    }

    public bool getIsStunned()
    {
        return stunned;
    }

    // Use this for initialization
    void Start()
    {
        mSpeed = 2f;
        Debug.Log("GAME START!");

        ochSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            // stun effect
        }
        else
        {
            Move();
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

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.15F);
        dash = 2;
        yield return new WaitForSeconds(1F);
        dash = 0;
    }

    IEnumerator stun(float time)
    {
        if (!stunned)
        {
            stunned = true;
            yield return new WaitForSeconds(time/2);
            stunned = false;
        }
    }
}

        
