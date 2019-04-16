using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {

    // Variables
    //[SerializeField]
    //private GameObject Power;
    [SerializeField]
    private GameObject ON;
    [SerializeField]
    private GameObject OFF;
    private Collider2D c2d;
    private bool powObj = false, plyr = false, enemy = false;
    private List<GameObject> coList;

    // Use this for initialization
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        coList = new List<GameObject>();
        c2d = GetComponent<Collider2D>();
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Enemy"))
        {
            coList.Add(collision.gameObject);
            ON.SetActive(false);
            OFF.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            coList.Remove(collision.gameObject);
        }

        if (coList.Count == 0)
        {
            ON.SetActive(true);
            OFF.SetActive(false);
        }
    }
}
