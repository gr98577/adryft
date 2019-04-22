using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker2 : MonoBehaviour {

    private float delay;
    private SpriteRenderer tr;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<SpriteRenderer>();
        StartCoroutine(onOff());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator onOff()
    {
        while (true)
        {
            delay = Random.Range(0.001f, 0.5f);
            yield return new WaitForSeconds(delay);
            if (tr.enabled)
            {
                tr.enabled = false;
            }
            else
            {
                tr.enabled = true;
            }
        }
    }
}
