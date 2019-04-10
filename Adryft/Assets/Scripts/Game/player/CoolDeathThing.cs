using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Video;

public class CoolDeathThing : MonoBehaviour {

    public PostProcessingProfile normal;
    private bool dead;
    private int stage;
    private GameObject player;
    [SerializeField]
    private float rate;
    [SerializeField]
    private GameObject videoPlayer;
    [SerializeField]
    private GameObject HUD;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stage = 0;
        GrainModel.Settings Grain = normal.grain.settings;
        Grain.intensity = 0;
        normal.grain.settings = Grain;
    }

    private void OnPlayerDeath()
    {
        videoPlayer.GetComponent<VideoPlayer>().Prepare();
        GetComponent<MenuController>().Close();
        GetComponent<MenuController>().enabled = false;
        stage = 1;
    }

    // Update is called once per frame
    void Update () {
		if (stage == 0)
        {
            if (player.GetComponent<damageController>().getHealth() <= 0)
            {
                OnPlayerDeath();
            }
        }
        else if (stage == 1)
        {
            GrainModel.Settings Grain = normal.grain.settings;
            //float time = Time.unscaledDeltaTime;
            //Debug.Log(Grain.intensity + " | " + time);
            Grain.intensity += Time.unscaledDeltaTime;
            if (Grain.intensity >= 1)
            {
                stage = 2;
                Grain.intensity = 1;
            }
            normal.grain.settings = Grain;
        }
        else if (stage == 2)
        {
            stage = 3;
            videoPlayer.GetComponent<VideoPlayer>().Play();
            HUD.SetActive(false);
        }
        else if (stage == 3)
        {
            GrainModel.Settings Grain = normal.grain.settings;
            Grain.intensity -= Time.unscaledDeltaTime * 5;
            if (Grain.intensity <= 0)
            {
                Grain.intensity = 0;
            }
            normal.grain.settings = Grain;
        }
        else if (stage == 4)
        {

        }
	}
}
