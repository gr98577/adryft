using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {

    private GameObject camera;
    private Camera cam;
    private GameObject player;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject endLocation;
    [SerializeField]
    private GameObject On;
    [SerializeField]
    private GameObject Off;
    private bool inCutscene;
    private float startTime;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        inCutscene = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (inCutscene)
        {
            float t = (Time.time - startTime) * 2;
            cam.orthographicSize = Mathf.SmoothStep(2f, 0.8f, t);
        }
        else if (cam.orthographicSize != 2f)
        {
            float t = (Time.time - startTime) * 3;
            cam.orthographicSize = Mathf.SmoothStep(0.8f, 2f, t);
            if (cam.orthographicSize == 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it collides with the player
        if (collision.CompareTag("Player"))
        {
            inCutscene = true;
            startTime = Time.time;
            camera.GetComponent<CameraController>().setCSL(target);
            player.GetComponentInParent<CutsceneController>().inCutscene = true;
            StartCoroutine(cutscene());
            On.SetActive(true);
            Off.SetActive(false);
        }
    }

    private IEnumerator cutscene()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2f);
        player.GetComponentInParent<CutsceneController>().inCutscene = false;
        player.transform.position = endLocation.transform.position;
        inCutscene = false;
        startTime = Time.time;
    }
}
