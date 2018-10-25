using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    // Variables
    [SerializeField]
    private GameObject health;
    [SerializeField]
    private GameObject ammo;
    private GameObject[] pickups;
    private int size;

    // Use this for initialization
    void Start () {
        // Creates an array from the given pickups then picks a random one
        pickups = new GameObject[] { health, ammo };
        size = pickups.Length;
        int rand = Random.Range(0, size);

        // Drops the selected pickup then self destructs
        var drop = Instantiate(pickups[rand], transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
