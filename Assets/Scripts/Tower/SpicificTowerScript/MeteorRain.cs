using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : BaseTower {

    public int fireAmount;
    public GameObject pinShot, laser, empty;
    public bool activeFire;

    public void Update()
    {
        GameObject enemy = TargetDetect(transform.position, false);
        if(!activeFire && enemy)
        {
            StartCoroutine(Fire(enemy.transform.position));
        }
    }

    public IEnumerator Fire(Vector3 position)
    {
        yield return null;
    }

}
