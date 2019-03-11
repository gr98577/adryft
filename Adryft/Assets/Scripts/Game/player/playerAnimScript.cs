using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimScript : MonoBehaviour {

    private SpriteRenderer sr;
    private Sprite front;
    private Sprite back;
    private Sprite[] fWalk = new Sprite[3], bWalk = new Sprite[3], lWalk = new Sprite[3], rWalk = new Sprite[3];

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        fWalk[0] = Resources.Load<Sprite>("PlayerAnim/up/1");
        fWalk[1] = Resources.Load<Sprite>("PlayerAnim/up/2");
        fWalk[2] = Resources.Load<Sprite>("PlayerAnim/up/3");

        bWalk[0] = Resources.Load<Sprite>("playerAnim/down/1");
        bWalk[1] = Resources.Load<Sprite>("playerAnim/down/2");
        bWalk[2] = Resources.Load<Sprite>("playerAnim/down/3");

        lWalk[0] = Resources.Load<Sprite>("playerAnim/left/1");
        lWalk[1] = Resources.Load<Sprite>("playerAnim/left/2");
        lWalk[2] = Resources.Load<Sprite>("playerAnim/left/3");

        rWalk[0] = Resources.Load<Sprite>("playerAnim/right/1");
        rWalk[1] = Resources.Load<Sprite>("playerAnim/right/2");
        rWalk[2] = Resources.Load<Sprite>("playerAnim/right/3");

        front = Resources.Load<Sprite>("PlayerAnim/PlayerFront");
        back = Resources.Load<Sprite>("PlayerAnim/PlayerBack");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
        // Sets its location to the player's and put itself on top of or under the player depending on mouse location
       /* if (mousePosition.y >= transform.position.y)
        {
            sr.sprite = back;
        }
        else
        {
            sr.sprite = front;
        }
        */
        
        if(mousePosition.y )


    }
}
