using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGscript : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject notif;
    private AudioSource thrust;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        notif.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collided with the player
        if (collision.gameObject == player)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            player.GetComponent<playerController>().zeroG = true;
            player.GetComponent<playerAnimScript>().zeroG = true;
            notif.SetActive(true);
            thrust.Play();
            player.GetComponent<TrailRenderer>().enabled = true;
            player.GetComponent<TrailRenderer>().time = 0.75f;
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().zeroG = true;
            //collision.GetComponent<Enemy2Controller>().zeroG = true;
            //collision.GetComponent<SlugController>().zeroG = true;
            collision.GetComponent<Rigidbody2D>().drag = 0f;
        }
        if (collision.CompareTag("PowerBlock"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = 0f;
            rb.freezeRotation = false;
            collision.GetComponent<PowerBox>().setZeroG(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If it collided with the player
        if (collision.gameObject == player)
        {
            player.GetComponent<playerController>().zeroG = false;
            player.GetComponent<playerAnimScript>().zeroG = false;
            notif.SetActive(false);
            thrust.Pause();
            player.GetComponent<TrailRenderer>().enabled = false;
            player.GetComponent<TrailRenderer>().time = 0.075f;

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().zeroG = false;
            //collision.GetComponent<Enemy2Controller>().zeroG = true;
            //collision.GetComponent<SlugController>().zeroG = true;
            collision.GetComponent<Rigidbody2D>().drag = 1f;
        }
        if (collision.CompareTag("PowerBlock"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = 1000f;
            rb.freezeRotation = true;
            collision.GetComponent<PowerBox>().setZeroG(true);
        }
    }
}
