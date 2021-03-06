﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private float openSpeed;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera menuCamera;

    private Rect mainRect;
    private Rect menuRect;

    private bool isMenuOpen;
    private bool shouldItBe;

    [SerializeField]
    private float SP;

    [SerializeField]
    private GameObject mainHUD;
    private RectTransform hudRect;

	// Use this for initialization
	void Start () {
        mainCamera = mainCamera.GetComponent<Camera>();
        menuCamera = menuCamera.GetComponent<Camera>();

        mainRect = mainCamera.rect;
        menuRect = menuCamera.rect;

        mainRect.x = 0f;
        mainRect.width = 1f;
        menuRect.x = -SP;
        menuRect.width = SP;

        mainCamera.rect = mainRect;
        menuCamera.rect = menuRect;

        isMenuOpen = false;
        shouldItBe = false;

        hudRect = mainHUD.GetComponent<RectTransform>();
        hudRect.localScale = new Vector3(5f, 5f, 1f);
	}

    private void Update()
    {
        if (GetComponentInParent<CutsceneController>().inCutscene)
        {
            shouldItBe = false;
        }
        else if (Input.GetButtonDown("Open Menu"))
        {
            if (shouldItBe)
            {
                shouldItBe = false;
            }
            else
            {
                shouldItBe = true;
            }
        }

        if (isMenuOpen && !shouldItBe)
        {
            Close();
        }
        else if (!isMenuOpen && shouldItBe)
        {
            Open();
        }
    }

    void Open()
    {
        Vector3 tempV = new Vector3(mainRect.x, mainRect.width, menuRect.x);
        Vector3 goalV = new Vector3(SP, 1f - SP, 0f);
        tempV = Vector3.Lerp(tempV, goalV, openSpeed);

        mainRect.x = tempV.x;
        mainRect.width = tempV.y;
        menuRect.x = tempV.z;

        mainCamera.rect = mainRect;
        menuCamera.rect = menuRect;

        hudRect.localScale = new Vector3(3.5f, 3.5f, 1f);

        if (tempV == goalV)
        {
            isMenuOpen = true;
        }
    }

    public void Close()
    {
        Vector3 tempV = new Vector3(mainRect.x, mainRect.width, menuRect.x);
        Vector3 goalV = new Vector3(0f, 1f, -SP);
        tempV = Vector3.Lerp(tempV, goalV, openSpeed);

        mainRect.x = tempV.x;
        mainRect.width = tempV.y;
        menuRect.x = tempV.z;

        mainCamera.rect = mainRect;
        menuCamera.rect = menuRect;

        hudRect.localScale = new Vector3(5f, 5f, 1f);

        if (tempV == goalV)
        {
            isMenuOpen = false;
        }
    }
}
