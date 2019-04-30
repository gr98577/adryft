using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    public GameObject creator;
    private Vector3 rPos;
    private float eTime;

	// Use this for initialization
	void Start () {
        rPos = transform.position - creator.transform.position;
        eTime = 0f;
        int x = Random.Range(0, 3);
        if (x != 0)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        eTime += Time.deltaTime / 10f;
        transform.position = creator.transform.position + rPos;
        transform.position = new Vector3(transform.position.x, transform.position.y + eTime, transform.position.z);
	}
}
