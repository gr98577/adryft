using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour {

    [SerializeField]
    private float time;
    [SerializeField]
    private GameObject decal;

    private SpriteRenderer sr;
    private Sprite[] es;
    

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();

        es = new Sprite[7];
        es[0] = Resources.Load<Sprite>("Explosion/1");
        es[1] = Resources.Load<Sprite>("Explosion/2");
        es[2] = Resources.Load<Sprite>("Explosion/3");
        es[3] = Resources.Load<Sprite>("Explosion/4");
        es[4] = Resources.Load<Sprite>("Explosion/5");
        es[5] = Resources.Load<Sprite>("Explosion/6");
        es[6] = Resources.Load<Sprite>("Explosion/7");
        sr.sprite = es[0];

        GameObject DeathBlood = Instantiate(decal, transform.position, Quaternion.identity);

        StartCoroutine(Explode());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(time);
        sr.sprite = es[1];
        yield return new WaitForSeconds(time);
        sr.sprite = es[2];
        yield return new WaitForSeconds(time);
        sr.sprite = es[3];
        yield return new WaitForSeconds(time);
        sr.sprite = es[4];
        yield return new WaitForSeconds(time);
        sr.sprite = es[5];
        yield return new WaitForSeconds(time);
        sr.sprite = es[6];
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
