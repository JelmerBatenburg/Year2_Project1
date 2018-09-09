using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCrossbowTower : BaseTower {

    public Transform RotatingPart;
    public bool activeFire;
    public Transform reloadLocation;
    public GameObject arrowPrefab;

    public void Update()
    {
        InfoPopUp();
        GameObject[] targets = TargetDetect(RotatingPart.position+ Vector3.up * 1.5f);
        if(targets.Length > 0)
        {
            RotatingPart.LookAt(targets[0].transform.position);
            if (!activeFire)
            {
                StartCoroutine(Fire());
            }
            activeFire = true;
        }
        else
        {
            activeFire = false;
        }
    }

    public IEnumerator Fire()
    {
        //fire
        yield return new WaitForSeconds(fireRate);
        //load
        if (activeFire)
        {
            StartCoroutine(Fire());
        }
    }
}
