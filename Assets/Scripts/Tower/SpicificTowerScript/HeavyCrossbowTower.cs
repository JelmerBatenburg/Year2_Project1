using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCrossbowTower : BaseTower {

    public CrossBowBolt bolt;
    public float boltSpeed;
    public Transform RotatingPart, rotatingPart2;
    public bool activeFire;
    public Transform reloadLocation;
    public GameObject boltPrefab;

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
            GameObject enemy = TargetDetect(RotatingPart.position + Vector3.up * 1.5f, true);
            if (enemy)
            {
                rotatingPart2.LookAt(new Vector3(enemy.transform.position.x, rotatingPart2.position.y, enemy.transform.position.z));
                RotatingPart.LookAt(enemy.transform.position);
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
        Destroy(bolt.gameObject, 5);
        bolt.transform.parent = null;
        bolt.speed = boltSpeed;
        yield return new WaitForSeconds(fireRate);
        GameObject tempBolt = Instantiate(boltPrefab, reloadLocation.position, reloadLocation.rotation, reloadLocation);
        bolt = tempBolt.GetComponent<CrossBowBolt>();
        bolt.damage = Mathf.RoundToInt(damage);
        activeFire = false;
    }
}
