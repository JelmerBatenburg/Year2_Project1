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
        if (!currentTarget)
        {

            animationController.SetBool("Attacking", false);
        }
        TargetDetection();
        Attack();
        Move();
    }

    public override IEnumerator AttackCoroutine()
    {
        animationController.SetBool("Attacking", true);
        attacking = true;
        yield return new WaitForEndOfFrame();
        GameObject g = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Destroy(g.gameObject, 2);
        if (currentTarget)
        {
            g.transform.LookAt(new Vector3(currentTarget.position.x, g.transform.position.y, currentTarget.position.z));
        }
        else
        {
            g.transform.LookAt(transform.position + transform.forward * 20);
        }
        RangedProjectile p = g.GetComponent<RangedProjectile>();
        p.damage = damage;
        p.target = currentTarget;
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }
}
