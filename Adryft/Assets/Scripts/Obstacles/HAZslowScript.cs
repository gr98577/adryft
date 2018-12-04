using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAZslowScript : MonoBehaviour {

    [SerializeField]
    private GameObject hpc;
    [SerializeField]
    private int numToSpawn;
    private GameObject player;
    private playerController pc;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();

        Vector3 origin = transform.position;
        float minX = origin.x - 0.2f;
        float maxX = origin.x + 0.2f;
        float minY = origin.y - 0.2f;
        float maxY = origin.y + 0.2f;

        for (int i = 0; i < numToSpawn; i++)
        {
            Vector3 loc = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject temp = Instantiate(hpc, loc, Quaternion.identity);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.isSlowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.isSlowed = false;
        }
    }
}
