using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{

    private bool isInUse;
    private SpriteRenderer sr;
    private Sprite[] es;
    private int i;

    public void setUse(bool isUsing)
    {
        isInUse = isUsing;
    }

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        es = new Sprite[6];
        es[0] = Resources.Load<Sprite>("ElectroBox/B0");
        es[1] = Resources.Load<Sprite>("ElectroBox/B1");
        es[2] = Resources.Load<Sprite>("ElectroBox/B2");
        es[3] = Resources.Load<Sprite>("ElectroBox/B3");
        es[4] = Resources.Load<Sprite>("ElectroBox/B4");
        es[5] = Resources.Load<Sprite>("ElectroBox/B5");
        sr.sprite = es[0];

        i = 0;

        StartCoroutine(Shock());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shock()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (isInUse)
            {
                sr.sprite = es[0];
            }
            else
            {
                i++;
                if (i >= 6) i = 1;
                sr.sprite = es[i];
            }
        }
    }
}