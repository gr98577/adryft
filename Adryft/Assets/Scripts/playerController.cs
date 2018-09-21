using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public AudioSource ochSource;


    //variables
    float mSpeed;
    int health;

    public GameObject gameOver;

    // Use this for initialization
    void Start () {
        mSpeed = .03f;
        health = 20;
        Debug.Log("GAME START!");

        ochSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(mSpeed * Input.GetAxisRaw("Horizontal"), mSpeed * Input.GetAxisRaw("Vertical"), 0f);

        if (health <= 0)
        {
            var clone = Instantiate(gameOver, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyProjectile"))
        {
            //health
            health = health - 2;
            //hurt sound
            ochSource.Play();


        }
    }
}
