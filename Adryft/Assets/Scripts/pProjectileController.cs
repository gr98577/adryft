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

        Vector3 pos = new Vector3(
            transform.position.x,
            transform.position.y,
            0);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator life()
    {
        yield return new WaitForSeconds(5F);
        Destroy(this.gameObject);
    }
}

