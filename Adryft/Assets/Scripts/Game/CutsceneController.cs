using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

    public bool inCutscene;

	// Use this for initialization
	void Start () {
        inCutscene = false;
	}

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            inCutscene = true;
        }
    }
}
