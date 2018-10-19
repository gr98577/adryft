using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    playerController pc;
    Image staminaBar;

    // Use this for initialization
    void Start()
    {
        staminaBar = GetComponent<Image>();
        pc = player.GetComponent<playerController>();
        staminaBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        pc = player.GetComponent<playerController>();
        staminaBar.fillAmount = pc.getStamina() / pc.getMaxStamina();
    }
}
