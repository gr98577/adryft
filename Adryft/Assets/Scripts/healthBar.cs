using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {


    Image HealthBar;

	// Use this for initialization
	void Start () {
        HealthBar = GetComponent<Image>();
        HealthBar.fillAmount = playerController.playerHealth;
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.fillAmount = (float)(playerController.playerHealth) / (playerController.startingPlayerHealth);
    }
}
