using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If it's triggered by the player or an enemy
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            // Deals falling damage if the target can't fly.
            damageController dc = collision.gameObject.GetComponent<damageController>();
            if (!dc.getFlying())
            {
                dc.doDamage(0, "fall", transform.position, -1.5f);
            }
        }
    }

    // Trigger detection 
    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
