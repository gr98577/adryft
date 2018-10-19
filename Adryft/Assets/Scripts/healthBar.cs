using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    damageController dc;
    Image HealthBar;

    // Use this for initialization
    void Start () {
        HealthBar = GetComponent<Image>();
        dc = player.GetComponent<damageController>();
        HealthBar.fillAmount = dc.getHealth();
	}
	
	// Update is called once per frame
	void Update () {
        dc = player.GetComponent<damageController>();
        HealthBar.fillAmount = (float)dc.getHealth() / dc.getMaxHealth();
    }
}
