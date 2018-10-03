using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : BaseUnit {

    public GameObject projectile;
    public Transform firePoint;

    public void Start()
    {
        FirstCheck();
    }

    public void Update()
    {
        TargetDetection();
        Attack();
        Move();
    }

    public override IEnumerator AttackCoroutine()
    {
        attacking = true;
        GameObject g = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Destroy(g.gameObject, 2);
        g.transform.LookAt(new Vector3(currentTarget.position.x,g.transform.position.y,currentTarget.position.z));
        RangedProjectile p = g.GetComponent<RangedProjectile>();
        p.damage = damage;
        p.target = currentTarget;
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }
}
