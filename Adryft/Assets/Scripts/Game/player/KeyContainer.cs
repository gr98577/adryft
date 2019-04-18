using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyContainer : MonoBehaviour {

    [SerializeField]
    private GameObject BlueUI;
    [SerializeField]
    private GameObject GreenUI;
    [SerializeField]
    private GameObject YellowUI;
    private bool BlueKey;
    private bool GreenKey;
    private bool YellowKey;

    public bool hasKey(int color)
    {
        switch (color)
        {
            case 1:
                return BlueKey;
            case 2:
                return GreenKey;
            case 3:
                return YellowKey;
            default:
                return false;
        }
    }

    // Use this for initialization
    void Start()
    {
        BlueUI.SetActive(false);
        GreenUI.SetActive(false);
        YellowUI.SetActive(false);
        BlueKey = false;
        GreenKey = false;
        YellowKey = false;
    }

    public void Blue()
    {
        BlueUI.SetActive(true);
        BlueKey = true;
    }

    public void Green()
    {
        GreenUI.SetActive(true);
        GreenKey = true;
    }

    public void Yellow()
    {
        YellowUI.SetActive(true);
        YellowKey = true;
    }
}
