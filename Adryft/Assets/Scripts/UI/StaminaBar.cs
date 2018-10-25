using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour {

    // Variables
    private GameObject player;

    playerController pc;
    Image staminaBar;

    // Use this for initialization
    void Start()
    {
        // Finds the player and its script
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        // Initializes the health bar
        staminaBar = GetComponent<Image>();
        staminaBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the fill of the bar to be equal to the % of health
        staminaBar.fillAmount = pc.getStamina() / pc.getMaxStamina();
    }
}
