using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //variables
    public float mSpeed;
    public int health;
    // Use this for initialization
    void Start () {
        mSpeed = .03f;
        health = 20;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(mSpeed * Input.GetAxisRaw("Horizontal"), mSpeed * Input.GetAxisRaw("Vertical"), 0f);

        //original: transform.Translate(mSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, mSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0f);
    }
}
