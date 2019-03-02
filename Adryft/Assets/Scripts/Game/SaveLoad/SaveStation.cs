using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour {

    private playerController pc;
    [SerializeField]
    private GameObject box;

	// Use this for initialization
	void Start () {
        box.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Use1"))
        {
            SaveGame();
        }
        if (Input.GetButtonDown("Use2"))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        SaveLoadManager.SavePlayer(pc);
    }

    public void LoadGame()
    {
        SaveLoadManager.LoadPlayer(pc);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            pc = collision.gameObject.GetComponent<playerController>();

            box.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            box.SetActive(false);
        }
    }
}
