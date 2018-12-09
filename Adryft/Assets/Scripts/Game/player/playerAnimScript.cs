using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimScript : MonoBehaviour {

    private SpriteRenderer sr;
    private Sprite front;
    private Sprite back;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        front = Resources.Load<Sprite>("PlayerAnim/PlayerFront");
        back = Resources.Load<Sprite>("PlayerAnim/PlayerBack");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Sets its location to the player's and put itself on top of or under the player depending on mouse location
        if (mousePosition.y >= transform.position.y)
        {
            sr.sprite = back;
        }
        else
        {
            sr.sprite = front;
        }
    }
}
