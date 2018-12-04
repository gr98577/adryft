using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPlantController : MonoBehaviour {

    private bool flip;
    private float time;
    private Vector3 backUp;

	// Use this for initialization
	void Start () {
        int temp = (int)Random.Range(0, 2);
        if (temp < 1) flip = false;
        else flip = true;
        backUp = new Vector3(0.0f, 1.0f, 0.0f);
        time = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {

        transform.up = Vector3.Lerp(transform.up, backUp, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float ammount = Random.Range(0.2f, 0.4f);
            int temp = (int)Random.Range(0, 2);
            if (temp < 1)
            {
                //(0.3, 1.0, 0.0)
                transform.up = new Vector3(ammount, 1.0f, 0.0f);
            }
            else
            {
                Debug.Log(transform.up);
                transform.up = new Vector3(-ammount, 1.0f, 0.0f);
            }
        }
    }
}
