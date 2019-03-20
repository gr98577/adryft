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

        if (Time.timeScale != 0f)
        { 
            float angle = FindAngle();
            //Debug.Log(angle);

            //Debug.Log(mousePosition);
            if (angle < 45 || angle > 315) //right
            {
                //Debug.Log("right");
                sr.sprite = rWalk[1];
                /*
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
                */
            }
            else if (angle >= 45 && angle <= 135) //up
            {
                //Debug.Log("up");
                sr.sprite = fWalk[1];
                /*
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
                */
            }

            else if (angle > 135 && angle < 225) //left
            {
                //Debug.Log("left");
                sr.sprite = lWalk[1];
                /*
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
                */
            }

            else if (angle >= 225 && angle <= 315) //down
            {
                //Debug.Log("down");
                sr.sprite = bWalk[1];
                /*
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
                */
            }
        }
    }

    private float FindAngle()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float x = mousePosition.x - transform.position.x;
        float y = mousePosition.y - transform.position.y;
        float angle = 0.0f;

        if (mousePosition.x > transform.position.x && mousePosition.y >= transform.position.y) // 1st Quadrant
        {
            angle = Mathf.Atan(y / x);
            angle *= Mathf.Rad2Deg;
        }
        if (mousePosition.x <= transform.position.x && mousePosition.y > transform.position.y) // 2nd Quadrant
        {
            angle = Mathf.Atan(x / y);
            angle *= Mathf.Rad2Deg;
            angle *= -1;
            angle += 90.0f;
        }
        if (mousePosition.x < transform.position.x && mousePosition.y <= transform.position.y) // 3rd Quadrant
        {
            angle = Mathf.Atan(y / x);
            angle *= Mathf.Rad2Deg;
            angle += 180.0f;
        }
        if (mousePosition.x >= transform.position.x && mousePosition.y < transform.position.y) // 4th Quadrant
        {
            angle = Mathf.Atan(x / y);
            angle *= Mathf.Rad2Deg;
            angle *= -1;
            angle += 270.0f;
        }

        return angle;
    }
    
    /*
    IEnumerator walkCycle(DIR d)
    {




        yield return new WaitForSeconds(1f);


    }
    */
}
