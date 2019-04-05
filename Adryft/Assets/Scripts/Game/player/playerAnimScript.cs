using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerAnimScript : MonoBehaviour {

    private SpriteRenderer sr;
    private Sprite front;
    private Sprite back;
    private Sprite[] fWalk = new Sprite[3], bWalk = new Sprite[3], lWalk = new Sprite[3], rWalk = new Sprite[3];
    private enum DIR { LEFT, RIGHT, UP, DOWN };
    private Vector3 oldPos;
    private int walkStage;
 
    // Use this for initialization
    void Start () {
        walkStage = 0;

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
        StartCoroutine(walkCycle());
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
                sr.sprite = rWalk[walkStage];
            }
            else if (angle >= 45 && angle <= 135) //up
            {
                sr.sprite = fWalk[walkStage];
            }

            else if (angle > 135 && angle < 225) //left
            {
                sr.sprite = lWalk[walkStage];
            }

            else if (angle >= 225 && angle <= 315) //down
            {
                sr.sprite = bWalk[walkStage];
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
    
    
    IEnumerator walkCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position != oldPos)
            {
                walkStage++;
                if (walkStage > 2)
                {
                    walkStage = 0;
                }
            }
            else
            {
                walkStage = 1;
            }

            oldPos = transform.position;
        }
    }   
}
