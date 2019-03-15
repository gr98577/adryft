using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerAnimScript : MonoBehaviour {

    private SpriteRenderer sr;
    private Sprite front;
    private Sprite back;
    private Sprite[] fWalk = new Sprite[3], bWalk = new Sprite[3], lWalk = new Sprite[3], rWalk = new Sprite[3];
    private enum DIR { LEFT, RIGHT, UP, DOWN };
    




    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        fWalk[0] = Resources.Load<Sprite>("PlayerAnim/up/1");
        fWalk[1] = Resources.Load<Sprite>("PlayerAnim/up/2");
        fWalk[2] = Resources.Load<Sprite>("PlayerAnim/up/3");

        bWalk[0] = Resources.Load<Sprite>("PlayerAnim/down/1");
        bWalk[1] = Resources.Load<Sprite>("PlayerAnim/down/2");
        bWalk[2] = Resources.Load<Sprite>("PlayerAnim/down/3");

        lWalk[0] = Resources.Load<Sprite>("PlayerAnim/left/1");
        lWalk[1] = Resources.Load<Sprite>("PlayerAnim/left/2");
        lWalk[2] = Resources.Load<Sprite>("PlayerAnim/left/3");

        rWalk[0] = Resources.Load<Sprite>("PlayerAnim/right/1");
        rWalk[1] = Resources.Load<Sprite>("PlayerAnim/right/2");
        rWalk[2] = Resources.Load<Sprite>("PlayerAnim/right/3");

        front = Resources.Load<Sprite>("PlayerAnim/PlayerFront");
        back = Resources.Load<Sprite>("PlayerAnim/PlayerBack");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Sets its location to the player's and put itself on top of or under the player depending on mouse location
        /*if (mousePosition.y >= transform.position.y)
         {
             sr.sprite = back;
         }
         else
         {
             sr.sprite = front;
         }
         */

        Debug.Log(mousePosition);
        if (System.Math.Round(mousePosition.x) == 0f && System.Math.Round(mousePosition.y) == 0f) //right
        {
            Debug.Log("here");
            sr.sprite = rWalk[1];
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (sr.sprite == rWalk[1])
                {
                    Debug.Log("here1");
                    sr.sprite = rWalk[2];
                }
                else if (sr.sprite == rWalk[2])
                {
                    Debug.Log("here2");
                    sr.sprite = rWalk[0];
                }
                else if (sr.sprite == rWalk[0])
                {
                    Debug.Log("here3");
                    sr.sprite = rWalk[1];
                }

            }

        }
        else if (System.Math.Round(mousePosition.x) == 0f && System.Math.Round(mousePosition.y) > 0f) //up
        {
            Debug.Log("here");
            sr.sprite = fWalk[1];
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (sr.sprite == fWalk[1])
                {
                    Debug.Log("here1");
                    sr.sprite = fWalk[2];
                }
                else if (sr.sprite == fWalk[2])
                {
                    Debug.Log("here2");
                    sr.sprite = fWalk[0];
                }
                else if (sr.sprite == fWalk[0])
                {
                    Debug.Log("here3");
                    sr.sprite = fWalk[1];
                }

            }
        }
        else if (System.Math.Round(mousePosition.x) < 0f && System.Math.Round(mousePosition.y) == 0f) //left
        {
            
            sr.sprite = lWalk[1];
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (sr.sprite == lWalk[1])
                {
                    Debug.Log("here1");
                    sr.sprite = lWalk[2];
                }
                else if (sr.sprite == lWalk[2])
                {
                    Debug.Log("here2");
                    sr.sprite = lWalk[0];
                }
                else if (sr.sprite == lWalk[0])
                {
                    Debug.Log("here3");
                    sr.sprite = lWalk[1];
                }
            }


            }
        else if (System.Math.Round(mousePosition.x) == 0f && System.Math.Round(mousePosition.y) < 0f) //down
        {
            
            sr.sprite = bWalk[1];
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                if (sr.sprite == bWalk[1])
                {
                    Debug.Log("here");
                    sr.sprite = bWalk[2];
                }
                else if (sr.sprite == bWalk[2])
                {
                    Debug.Log("here2");
                    sr.sprite = bWalk[0];
                }
                else if (sr.sprite == bWalk[0])
                {
                    Debug.Log("here3");
                    sr.sprite = bWalk[1];
                }
            }



            }



    }

    IEnumerator walkCycle(DIR d)
    {




        yield return new WaitForSeconds(1f);


    }
}
