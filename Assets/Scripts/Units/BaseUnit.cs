using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {

    public PathFinding pathfinding;
    public LayerMask targetLayer;
    public bool friendly;
    public List<Node> path;
    public float movementSpeed, rotationSpeed, walkDistance;
    public Transform currentTarget;
    public float health, attackRange, damage, targetDetectionRange, attackSpeed;
    public bool ranged;
    public GameObject projectile;
    public bool attacking;
    private Transform enemyBase;

    public void FirstCheck()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        for (int i = 0; i < bases.Length; i++)
        {
            if (bases[i].GetComponent<Base>().friendly && !friendly || !bases[i].GetComponent<Base>().friendly && friendly)
            {
                currentTarget = bases[i].GetComponent<Base>().entrance;
                enemyBase = bases[i].GetComponent<Base>().entrance;
                StartCoroutine(RefreshPath());
                break;
            }
        }
    }

    public void Awake()
    {
        FirstCheck();
    }

    public void Update()
    {
        if (currentTarget)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) >= attackRange / 2)
            {
                currentTarget = TargetDetection();
                Move();
            }
            AttackCheck();
        }
        else
        {
            currentTarget = TargetDetection();
        }
    }

    public void AttackCheck()
    {
        if (currentTarget)
        {
            if (currentTarget.GetComponent<BaseUnit>() || currentTarget.GetComponent<BaseTower>())
            {
                if (Vector3.Distance(transform.position, currentTarget.gameObject.GetComponent<Collider>().ClosestPoint(transform.position)) <= attackRange)
                {
                    if (!attacking)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }
        }
    }

    public IEnumerator Attack()
    {
        attacking = true;
        if (ranged)
        {

        }
        else
        {
            if (currentTarget.GetComponent<BaseTower>())
            {
                BaseTower tower = currentTarget.GetComponent<BaseTower>();
                tower.health -= damage;
                tower.HealthCheck();
            }
            if (currentTarget.GetComponent<BaseUnit>())
            {
                BaseUnit unit = currentTarget.GetComponent<BaseUnit>();
                unit.health -= damage;
                unit.HealthCheck();
            }
        }
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }

    public void Move()
    {
        if (path != null && path.Count > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0].position, movementSpeed * Time.deltaTime);
            Vector3 targetPosition = path[0].position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPosition, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            if (Vector3.Distance(path[0].position, transform.position) < walkDistance)
            {
                path.RemoveAt(0);
            }
        }
        else
        {
            if(path != null && path.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[0].position, movementSpeed * Time.deltaTime);
                Vector3 targetPosition = path[0].position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPosition, rotationSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);

                if (Vector3.Distance(path[0].position, transform.position) < walkDistance/4)
                {
                    path.RemoveAt(0);
                }
            }
        }
    }

    public IEnumerator RefreshPath()
    {
        yield return new WaitForEndOfFrame();
        if (currentTarget)
        {
            if (currentTarget.gameObject.GetComponent<Collider>())
            {
                path = GameObject.FindWithTag("Manager").GetComponent<PathFinding>().FindPath(transform.position, currentTarget.gameObject.GetComponent<Collider>().ClosestPoint(transform.position));
            }
            else
            {
                path = GameObject.FindWithTag("Manager").GetComponent<PathFinding>().FindPath(transform.position, currentTarget.position);
            }
        }
    }

    public void HealthCheck()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Transform TargetDetection()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, targetDetectionRange, targetLayer);
        if(targetsInRange.Length > 0)
        {
            List<float> distances = new List<float>();
            for (int i = 0; i < targetsInRange.Length; i++)
            {
                distances.Add(Vector3.Distance(targetsInRange[i].transform.position, transform.position));
            }

            float distance = Mathf.Min(distances.ToArray());

            for (int i = 0; i < distances.Count; i++)
            {
                if(distance == distances[i])
                {
                    StartCoroutine(RefreshPath());
                    return targetsInRange[i].transform;
                }
            }
        }
        if(currentTarget != enemyBase)
        {
            StartCoroutine(RefreshPath());
            return enemyBase;
        }
        return enemyBase;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
