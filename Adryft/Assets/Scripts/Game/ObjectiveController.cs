using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {

    // Variables
    private GameObject player;
    [SerializeField]
    private GameObject success;
    [SerializeField]
    private GameObject mainAudioSource;
    private AudioSource mainAudio;
    [SerializeField]
    private GameObject sucessMusic;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<Pointer>().SetCheckpoint(gameObject);
        mainAudio = mainAudioSource.GetComponent<AudioSource>();
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            //var clone = Instantiate(success, player.transform.position, Quaternion.identity);
            success.SetActive(true);
            // self destructs
            //Destroy(player);

            mainAudio.mute = true;
            var clone = Instantiate(sucessMusic, transform.position, Quaternion.identity);

            Time.timeScale = 0f;
        }
    }
}
