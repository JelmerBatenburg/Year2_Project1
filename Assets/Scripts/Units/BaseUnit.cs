using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseUnit : MonoBehaviour {
    public NavMeshAgent agent;
    public bool friendly;
    public Transform currentTarget;
    public Transform enemyBase;
    public LayerMask targetLayer;
    public float health, attackRange, targetDetectionRange, attackSpeed, walkspeed;
    public int damage;
    public bool attacking, stopWalking;
    public Animator animationController;

    public void Move()
    {
        if (stopWalking)
        {
            transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));
            agent.speed = 0;
            animationController.SetBool("Walking", false);
        }
        else
        {
            agent.speed = walkspeed;
            animationController.SetBool("Walking", true);
        }
    }

    public void FirstCheck()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        for (int i = 0; i < bases.Length; i++)
        {
            if (bases[i].GetComponent<Base>().friendly && !friendly || !bases[i].GetComponent<Base>().friendly && friendly)
            {
                currentTarget = bases[i].GetComponent<Base>().entrance;
                enemyBase = bases[i].GetComponent<Base>().entrance;
                agent.SetDestination(enemyBase.position);
                break;
            }
        }
    }

    public void TargetDetection()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, targetDetectionRange, targetLayer);
        if (targetsInRange.Length > 0)
        {
            List<float> distances = new List<float>();
            for (int i = 0; i < targetsInRange.Length; i++)
            {
                distances.Add(Vector3.Distance(targetsInRange[i].transform.position, transform.position));
            }

            float distance = Mathf.Min(distances.ToArray());

            for (int i = 0; i < distances.Count; i++)
            {
                if (distance == distances[i])
                {
                    agent.SetDestination(targetsInRange[i].transform.position);
                    currentTarget = targetsInRange[i].transform;
                    agent.SetDestination(currentTarget.position);
                }
            }
        }
        if (!currentTarget)
        {
            currentTarget = enemyBase;
            agent.SetDestination(currentTarget.position);
        }
    }

    public virtual void Damage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        if (!attacking)
        {
            if (currentTarget && Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                stopWalking = true;
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                stopWalking = false;
            }
        }
    }

    public virtual IEnumerator AttackCoroutine()
    {
        animationController.SetBool("Attacking", true);
        attacking = true;
        yield return new WaitForEndOfFrame();
        animationController.SetBool("Attacking", false);
        if (currentTarget.parent.GetComponent<Base>())
        {
            currentTarget.parent.GetComponent<Base>().health -= damage;
            currentTarget.parent.GetComponent<Base>().HealthCheck();
        }
        else if (currentTarget.GetComponent<BaseTower>())
        {
            currentTarget.GetComponent<BaseTower>().health -= damage;
            currentTarget.GetComponent<BaseTower>().HealthCheck();
        }
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }
}