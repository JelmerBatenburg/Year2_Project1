﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : BaseTower {

    public int fireAmount;
    public GameObject pinShot, laser, empty;
    public bool activeFire;
    public Transform firePoint, rotationPoint;
    public float fireDelay;
    public GameObject currentpin;

    public void Update()
    {
        if (placed)
        {
            GameObject enemy = TargetDetect(transform.position, false);
            rotationPoint.LookAt(new Vector3(enemy.transform.position.x, rotationPoint.position.y, enemy.transform.position.z));
            if (!activeFire && enemy)
            {
                StartCoroutine(Fire(enemy.transform.position, ShotType.pinshot, false));
            }
        }
    }

    public enum ShotType { pinshot,laser}
    public IEnumerator Fire(Vector3 position,ShotType type,bool turnsOff)
    {
        activeFire = true;
        GameObject tempEmpty = Instantiate(empty, transform.position + (position - transform.position) / 2 + new Vector3(0, -1, 0), Quaternion.identity);
        tempEmpty.transform.LookAt(position);
        if (type == ShotType.pinshot)
        {
            currentpin = Instantiate(pinShot, firePoint.position, firePoint.rotation,tempEmpty.transform);
            currentpin.transform.Rotate(-90, 0, 0);
            currentpin.GetComponent<MeteorTowerPin>().tower = this;
        }
        else
        {
            GameObject pin = Instantiate(laser, firePoint.position, firePoint.rotation, tempEmpty.transform);
            Destroy(pin.transform.parent.gameObject, 2);
        }

        if (turnsOff)
        {
            yield return new WaitForSeconds(fireDelay);
            Destroy(currentpin);
            activeFire = false;
        }

    }

}