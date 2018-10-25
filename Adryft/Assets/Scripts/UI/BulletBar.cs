using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBar : MonoBehaviour {

    // Variables
    private GameObject player;

    playerController pc;
    Image bulletBar;

    // Use this for initialization
    void Start()
    {
        // Finds the player and its script
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        // Finds the bullet bar
        bulletBar = GetComponent<Image>();
        bulletBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the fill of the bar to be equal to the % of ammo
        bulletBar.fillAmount = (float)pc.getAmmunition() / pc.getMaxAmmo();
    }
}
