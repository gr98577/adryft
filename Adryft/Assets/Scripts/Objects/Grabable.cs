using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour {

    private GameObject player;
    private playerController pc;
    private Vector3 pos;
    private bool held;
    [SerializeField]
    private float reach = 0.35f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire3"))
        {
            held = false;
        }

        if (held)
        {
            LockToPlayer();
        }
    }

    private void OnMouseOver()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (Input.GetButtonDown("Fire3") && dist <= reach)
        {
            held = true;
            pos = transform.position - player.transform.position;
        }
    }

    private void LockToPlayer()
    {
        float tSpeed = pc.getTSpeed();
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(tSpeed * direction.normalized);

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= reach)
        {
            held = false;
        }
    }
}
