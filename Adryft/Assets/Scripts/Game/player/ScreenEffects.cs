using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ScreenEffects : MonoBehaviour {

    public PostProcessingProfile normal;
    private playerController pc;
    private damageController dc;
    float stamina;
    float maxStamina;
    float health;
    float maxHealth;

    // Use this for initialization
    void Start () {
        pc = GetComponent<playerController>();
        dc = GetComponent<damageController>();

        ChromaticAberrationModel.Settings CAS = normal.chromaticAberration.settings;
        CAS.intensity = 0;
        normal.chromaticAberration.settings = CAS;

        UserLutModel.Settings ULS = normal.userLut.settings;
        ULS.contribution = 0;
        normal.userLut.settings = ULS;
    }
	
	// Update is called once per frame
	void Update () {
        stamina = pc.getStamina();
        maxStamina = pc.getMaxStamina();
        health = dc.getHealth();
        maxHealth = dc.getMaxHealth();

        ChromaticAberrationModel.Settings CAS = normal.chromaticAberration.settings;
        float temp = 1 - (stamina / maxStamina);
        CAS.intensity = temp * 2;
        normal.chromaticAberration.settings = CAS;

        float healthPC = health / maxHealth;
        if (healthPC <= 0.5f)
        {
            Debug.Log(healthPC);
            UserLutModel.Settings ULS = normal.userLut.settings;
            temp = (healthPC / 0.5f);
            ULS.contribution = 1 - Mathf.Clamp(temp, 0, 1);
            normal.userLut.settings = ULS;
        }
    }
}
