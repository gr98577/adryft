using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : MonoBehaviour {

    [SerializeField]
    private int amount;
    private float maxSize;
    private float minSize;
    private float size;
    private bool grow;

	// Use this for initialization
	void Start () {
        maxSize = 1.3f;
        minSize = 1.0f;
        size = minSize;
        grow = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (grow)
        {
            size += 0.002f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size >= maxSize) grow = false;
        }
        else
        {
            size -= 0.002f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size <= minSize) grow = true;
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageController dc = collision.gameObject.GetComponent<damageController>();
            if (dc.getHealth() != dc.getMaxHealth())
            {
                dc.giveHealth(amount);
                Destroy(this.gameObject);
            }
        }

    }
}
