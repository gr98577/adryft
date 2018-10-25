using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBar : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    playerController pc;
    Image bulletBar;

    // Use this for initialization
    void Start()
    {
        bulletBar = GetComponent<Image>();
        pc = player.GetComponent<playerController>();
        bulletBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        pc = player.GetComponent<playerController>();
        bulletBar.fillAmount = (float)pc.getAmmunition() / pc.getMaxAmmo();
        
    }
}
