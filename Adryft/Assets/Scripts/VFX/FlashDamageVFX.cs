using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamageVFX : MonoBehaviour {

    [SerializeField]
    float a;

    SpriteRenderer sr;
    private GameObject player;
    Color c1 = new Color(1,1,1,0);
    Color c2 = new Color(1,1,1,0);
    Color tmpColor;
    Vector3 playerPosition;

    bool peaked = false;

    // Use this for initialization
    void Start ()
    {
        c2.a = a;
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        sr.color = c1;
        playerPosition = player.transform.position;
        transform.position = playerPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (peaked){
            sr.color = Color.Lerp(sr.color, c1, 0.99f);

            if (sr.color.a == c1.a)
            {
                Debug.Log("A");
                Destroy(gameObject);
            }
        }
        else
        {
            sr.color = Color.Lerp(sr.color, c2, 0.99f);

            Debug.Log(c2.a + ":" + sr.color.a);
            if (sr.color.a == c2.a)
            {
                Debug.Log("B");
                peaked = true;
            }
        }

        followPlayer();
	}

    // Follows The Player
    private void followPlayer()
    {
        playerPosition = player.transform.position;
        transform.position = playerPosition;
    }
}
