using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

    private float minimum = 0.0f;
    private float maximum = 1f;
    private float duration = 1.0f;

    [SerializeField]
    private float radius;

    private bool faded = true;

    private float startTime;
    private SpriteRenderer sprite;

    private Sprite[] gas;

    void Start()
    {
        transform.position += Random.insideUnitSphere * radius;
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time;

        int x = Random.Range(0, 2);
        if (x == 0)
        {
            sprite.sprite = Resources.Load<Sprite>("slug/Gas1");
        }
        else if (x == 1)
        {
            sprite.sprite = Resources.Load<Sprite>("slug/Gas2");
        }
        else
        {
            sprite.sprite = Resources.Load<Sprite>("slug/Gas3");
        }
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;

        if (faded)
        {
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t * 2f));
            if (t > 1f)
            {
                faded = false;
                startTime = Time.time;
            }
        }
        else
        {
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, t / 1.5f));
            if (t > 1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
