using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

    private float maxSize;
    private float minSize;
    private float size;
    private bool grow;
    [SerializeField]
    private int color;
    [SerializeField]
    private GameObject sound;

	// Use this for initialization
	void Start () {
        maxSize = 1.3f;
        minSize = 1.0f;
        size = minSize;
        grow = true;
    }
	
	// Update is called once per frame
	void Update () {
        // Grows until it reaches max size, then shrinks
        if (grow)
        {
            size += 0.004f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size >= maxSize) grow = false;
        }
        // Shrinks until it reaches min sze then grows
        else
        {
            size -= 0.004f;
            transform.localScale = new Vector3(1, 1, 1) * size;
            if (size <= minSize) grow = true;
        }
    }

    // Called on colission
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collided with the player give the player helth and destroys itself
        if (collision.CompareTag("Player"))
        {
            switch (color)
            {
                case 1:
                    collision.GetComponent<KeyContainer>().Blue();
                    break;
                case 2:
                    collision.GetComponent<KeyContainer>().Green();
                    break;
                case 3:
                    collision.GetComponent<KeyContainer>().Yellow();
                    break;
                default:
                    break;
            }
            var clone = Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
