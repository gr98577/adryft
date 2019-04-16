using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject Opened;
    [SerializeField]
    private GameObject Closed;
    private bool isOpen;
    private AudioSource open;
    

	// Use this for initialization
	void Start () {
        Close();
        isOpen = false;
        UI.SetActive(false);
        open = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Use1") && UI.activeSelf)
        {
            if (isOpen)
            {
                open.Play();
                Close();
            }
            else
            {
                open.Play();
                Open();
            }
        }
    }

    public void Open()
    {
        isOpen = true;
        Closed.SetActive(false);
        Opened.SetActive(true);
    }

    public void Close()
    {
        isOpen = false;
        Closed.SetActive(true);
        Opened.SetActive(false);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Close();
            UI.SetActive(false);
        }
    }
}
