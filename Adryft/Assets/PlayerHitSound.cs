using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitSound : MonoBehaviour {

    private AudioSource hitSound;

	// Use this for initialization
	void Start () {
        hitSound = GetComponent<AudioSource>();
        int rand = Random.Range(1, 5);
        string path = "PlayerHit/playerhit_" + rand.ToString();
        hitSound.clip = Resources.Load<AudioClip>(path);
        hitSound.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (!hitSound.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
