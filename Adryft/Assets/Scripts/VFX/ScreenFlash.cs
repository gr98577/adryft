using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {

    private float minimum = 0.0f;
    [SerializeField]
    private float maximum = 1f;
    private float duration = 0.1f;

    private float r;
    private float g;
    private float b;

    private float t;

    private bool faded = true;

    private Image sprite;

    void Start()
    {
        sprite = GetComponentInChildren<Image>();

        t = 0;

        r = sprite.color.r;
        g = sprite.color.g;
        b = sprite.color.b;
    }

    void Update()
    {

        t += Time.unscaledDeltaTime / duration;

        if (faded)
        {
            sprite.color = new Color(r, g, b, Mathf.SmoothStep(minimum, maximum, t));
            if (t >= maximum)
            {
                faded = false;
                t = 0;
            }
        }
        else
        {
            sprite.color = new Color(r, g, b, Mathf.SmoothStep(maximum, minimum, t));
            if (t >= maximum)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
