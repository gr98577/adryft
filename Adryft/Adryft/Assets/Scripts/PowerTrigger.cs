using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTrigger : MonoBehaviour
{

    // Variables
    //[SerializeField]
    //private GameObject Power;
    [SerializeField]
    private GameObject ON;
    [SerializeField]
    private GameObject OFF;
    private bool powObj = false, plyr = false, enemy = false;
    private List<GameObject> coList;

    // Use this for initialization
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        coList = new List<GameObject>();
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("PowerBlock") || collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            coList.Add(collision.gameObject);
            ON.SetActive(true);
            OFF.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        coList.Remove(collision.gameObject);
        if (coList.Count == 0)
        {
            ON.SetActive(false);
            OFF.SetActive(true);
        }
    }
}
