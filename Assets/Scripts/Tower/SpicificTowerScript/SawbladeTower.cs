using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeTower : BaseTower {
    public Transform rotationPoint;
    public bool activeFire;
    public GameObject sawblade;
    public Transform firePoint;

    public void Start()
    {
        range.SetActive(true);
        SetRange();
    }

    public void Update()
    {
        if (placed)
        {
            InfoPopUp();
            GameObject enemy = TargetDetect(rotationPoint.position, false);
            if (enemy)
            {
                rotationPoint.LookAt(new Vector3(enemy.transform.position.x, rotationPoint.position.y, enemy.transform.position.z));
                if (!activeFire)
                {
                    StartCoroutine(Fire());
                }
            }
        }
    }

    public IEnumerator Fire()
    {
        GameObject blade = Instantiate(sawblade, firePoint.position, firePoint.rotation);
        blade.GetComponent<Sawblade>().damage = damage;
        Destroy(blade, 10);
        activeFire = true;
        yield return new WaitForSeconds(fireRate);
        activeFire = false;
    }
}
