using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasing : MonoBehaviour {

    private float speed;
    private bool atRest;
    private float offset;
    private GameObject player;

	// Use this for initialization
	void Start () {
        atRest = false;
        speed = Random.Range(0.08f, 0.12f);
        offset = Random.Range(-0.5f, 0.5f);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerController>().zeroG)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float dist = speed;
        transform.Translate(offset * dist, dist, 0f);

        speed /= 2f;
        if (speed <= 0.001f)
        {
            GetComponent<BulletCasing>().enabled = false;
        }
    }
}
