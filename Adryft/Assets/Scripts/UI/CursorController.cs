using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    [SerializeField]
    Texture2D cursorImage;
    [SerializeField]
    Texture2D cursorImageActive;
    [SerializeField]
    Vector2 hotSpot;
    [SerializeField]
    private CursorMode cursorMode = CursorMode.Auto;

    bool OnEnemy = false;
    bool OnElement = false;

    private GameObject finish;

    // Use this for initialization
    void Start () {
        transform.localScale *= 2000f / Screen.width;
        finish = GameObject.FindGameObjectWithTag("Finish");
        hotSpot = new Vector2(15f, 15f);
        Cursor.SetCursor(cursorImage, hotSpot, cursorMode);
    }

	// Update is called once per frame
	void LateUpdate () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0.0f;
        mousePosition.y -= 0.01f;

        transform.position = mousePosition;
        faceObjective();
        //transform.position = Vector3.Lerp(transform.position, mousePosition, 150 * Time.deltaTime);
    }

    private void faceObjective()
    {
        Vector2 direction = new Vector2(
                    finish.transform.position.x - transform.position.x,
                    finish.transform.position.y - transform.position.y);

        transform.up = direction;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnEnemy = true;

        if (collision.CompareTag("Enemy"))
        {
            Cursor.SetCursor(cursorImageActive, hotSpot, cursorMode);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnEnemy = false;

        if (collision.CompareTag("Enemy"))
        {
            Cursor.SetCursor(cursorImage, hotSpot, cursorMode);
        }
    }
}
