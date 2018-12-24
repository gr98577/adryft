using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAnim : MonoBehaviour {

    [SerializeField]
    private float time;

    private SpriteRenderer sr;
    private Sprite[] idle;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        idle = new Sprite[3];
        idle[0] = Resources.Load<Sprite>("AntiPlayer/AF1");
        idle[1] = Resources.Load<Sprite>("AntiPlayer/AF2");
        idle[2] = Resources.Load<Sprite>("AntiPlayer/AF3");
        StartCoroutine(stand());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator stand()
    {
        int i = 0;
        bool way = true;
        while (true)
        {
            yield return new WaitForSeconds(time);

            sr.sprite = idle[i];
            i++;
            if (i >= 3)
            {
                i = 0;
            }
        }
    }
}
