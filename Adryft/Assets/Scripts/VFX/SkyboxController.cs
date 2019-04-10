using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour {

    private GameObject mainCamera;
    private float z;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        z = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        float x = mainCamera.transform.position.x;
        float y = mainCamera.transform.position.y;
        transform.position = new Vector3(x, y, z);
	}
}
