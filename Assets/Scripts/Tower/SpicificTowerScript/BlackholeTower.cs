using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeTower : BaseTower {

    public float chargeTime;
    public GameObject blackHole;

    public Transform rotationPart1,rotationPart2,firepoint;
    public bool activeFire;

    public void Start()
    {
        range.SetActive(true);
        SetRange();
    }

    public void Update()
    {
        if (placed)
        {
            CheckIfAttacked();
            InfoPopUp();
            GameObject enemy = TargetDetect(firepoint.position, true);
            if (enemy)
            {
                rotationPart1.LookAt(new Vector3(enemy.transform.position.x, rotationPart1.position.y, enemy.transform.position.z));
                rotationPart2.LookAt(enemy.transform.position);
                if (!activeFire)
                {
                    StartCoroutine(Fire());
                    activeFire = true;
                }
            }
        }
    }

    public IEnumerator Fire()
    {
        source.PlayOneShot(FireSound, 3);
        GameObject hole = Instantiate(blackHole, firepoint.position, firepoint.rotation);
        hole.GetComponent<BlackHole>().damage = Mathf.RoundToInt(damage);
        Destroy(hole, 5);
        yield return new WaitForSeconds(chargeTime);
        activeFire = false;
    }
}
