using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenseFlareSizer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        LensFlare lf = GetComponentInChildren<LensFlare>();
        lf.brightness = lf.brightness * transform.localScale.x / 0.12f;
    }
}
