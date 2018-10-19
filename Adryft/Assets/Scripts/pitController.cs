using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Debug.Log("Works");
            damageController dc = collision.gameObject.GetComponent<damageController>();
            if (!dc.getFlying())
            {
                dc.doDamage(0, "fall", transform.position, -1.5f);
            }
        }
    }
}
