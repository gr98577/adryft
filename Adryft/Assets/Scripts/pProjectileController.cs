using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pProjectileController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(life());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0.05f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            damageController dc = collision.gameObject.GetComponent<damageController>();
            dc.doDamage(2, "none", transform.position);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            //spawn particle effect
            Destroy(this.gameObject);
        }
    }

    IEnumerator life()
    {
        yield return new WaitForSeconds(5F);
        Destroy(this.gameObject);
    }
}

