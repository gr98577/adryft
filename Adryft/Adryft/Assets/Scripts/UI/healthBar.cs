using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {

    // Variables
    private GameObject player;

    damageController dc;
    Image HealthBar;

    // Use this for initialization
    void Start ()
    {
        // Finds the player and its script
        player = GameObject.FindGameObjectWithTag("Player");
        dc = player.GetComponent<damageController>();
        // Initializes the health bar
        HealthBar = GetComponent<Image>();
        HealthBar.fillAmount = dc.getHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Sets the fill of the bar to be equal to the % of health
        HealthBar.fillAmount = (float)dc.getHealth() / dc.getMaxHealth();
    }
}
