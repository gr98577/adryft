using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordController : MonoBehaviour {

    // Variables
    [SerializeField]
    private GameObject player;

    private bool pickedUp = false;
    private bool canSwing = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if ( pickedUp == true)
        {
            canSwing = true;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            attachPlayer(mousePosition);
            faceMouse(mousePosition);
        }

        if (Input.GetButtonDown("Fire1") && canSwing)
        {
            StartCoroutine(Swing());
        }
    }

    void attachPlayer(Vector3 mouse)
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 position = new Vector3(0, 0, 0);


        if (mouse.y >= transform.position.y)
        {
            position = new Vector3(playerPosition.x, playerPosition.y, 1);
        }
        else
        {
            position = new Vector3(playerPosition.x, playerPosition.y, -1);
        }
        transform.position = position;
    }

    void faceMouse(Vector3 mouse)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
                    mouse.x - transform.position.x,
                    mouse.y - transform.position.y);
        transform.up = direction;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickedUp = true;
        }
    }

    IEnumerator Swing()
    {

        Debug.Log("weapong swung");
        //test
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        canSwing = false;
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 5, transform.rotation.z);
        yield return new WaitForSeconds(0.05F);
        //end test
        transform.Rotate(transform.rotation.x, transform.rotation.y - 35, transform.rotation.z);
        canSwing = true;
    }
}
