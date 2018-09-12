using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailgunTower : BaseTower {

    public Transform rotatingPart, rotatingPart2,fireLocation;
    public bool activeFire;
    public float nailSpeed;
    public GameObject nailPrefab;

    public void Update()
    {
        if (placed)
        {
            InfoPopUp();
            GameObject enemy = TargetDetect(rotatingPart.position, true);
            if (enemy)
            {
                rotatingPart2.LookAt(new Vector3(enemy.transform.position.x, rotatingPart2.position.y, enemy.transform.position.z));
                rotatingPart.LookAt(enemy.transform.position);
                if (!activeFire)
                {
                    StartCoroutine(Fire());
                }
            }
        }
    }

    public IEnumerator Fire()
    {
        activeFire = true;
        GameObject tempNail = Instantiate(nailPrefab, fireLocation.position, fireLocation.rotation);
        tempNail.GetComponent<CrossBowBolt>().speed = nailSpeed;
        Destroy(tempNail, 3);
        yield return new WaitForSeconds(fireRate);
        activeFire = false;
    }

}
