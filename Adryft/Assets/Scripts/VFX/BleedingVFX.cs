﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingVFX : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
