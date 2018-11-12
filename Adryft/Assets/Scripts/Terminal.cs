using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

    [SerializeField]
    private GameObject prompt;
    [SerializeField]
    private GameObject TextBox;
    [SerializeField]
    private string text;
    private GameObject clone;
    private bool opened;
    

	// Use this for initialization
	void Start () {
        opened = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(false);
            Destroy(clone);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Interact"))
        {
            clone = Instantiate(TextBox);
            //clone
        }
    }
}
