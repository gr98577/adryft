using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    [SerializeField]
    GameObject weapon;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player initially
        if (collision.CompareTag("Player"))
        {
            weapon.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
