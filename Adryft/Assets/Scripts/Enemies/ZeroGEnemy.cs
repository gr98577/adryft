using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGEnemy : MonoBehaviour {

	public void Make0G(bool should)
    {
        if (should)
        {
            gameObject.GetComponent<EnemyController>().zeroG = true;
        }
    }
}
