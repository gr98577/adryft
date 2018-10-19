using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    [SerializeField]
    private GameObject health;
    [SerializeField]
    private GameObject ammo;
    private GameObject[] pickups;
    private int size;

    // Use this for initialization
    void Start () {
        pickups = new GameObject[] { health, ammo };
        size = pickups.Length;
        int rand = Random.Range(0, size);
        //int drop = (int)(rand * size);
        //Debug.Log(rand + " | " + drop);
        var drop = Instantiate(pickups[rand], transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
