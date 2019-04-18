using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSplitter : MonoBehaviour {

    [SerializeField]
    private GameObject[] children;

    private void OnEnable()
    {
        foreach(GameObject x in children)
        {
            x.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject x in children)
        {
            x.SetActive(false);
        }
    }
}
