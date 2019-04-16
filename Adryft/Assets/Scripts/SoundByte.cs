using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByte : MonoBehaviour {

    private AudioSource caseSound;

	// Use this for initialization
	void Start () {
        caseSound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!caseSound.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
