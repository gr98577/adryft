using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    [SerializeField]
    Texture2D cursorImage;
    [SerializeField]
    Texture2D cursorImageActive;
    [SerializeField]
    Vector2 hotSpot;
    [SerializeField]
    private CursorMode cursorMode = CursorMode.Auto;
    private GameObject player;

    bool OnEnemy = false;
    bool OnElement = false;

    private GameObject finish;

    public void SetCheckpoint(GameObject checkpoint)
    {
        finish = checkpoint;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        transform.localScale *= 2000f / Screen.width;
        hotSpot = new Vector2(15f, 15f);
        Cursor.SetCursor(cursorImage, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position;

        Vector2 direction = new Vector2(
                    finish.transform.position.x - transform.position.x,
                    finish.transform.position.y - transform.position.y);

        transform.up = direction;

        if (finish.transform.position.y >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    private void faceObjective()
    {
        
    }
}
