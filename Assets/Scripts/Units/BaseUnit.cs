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

    public void FirstCheck()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        for (int i = 0; i < bases.Length; i++)
        {
            if (bases[i].GetComponent<Base>().friendly && !friendly || !bases[i].GetComponent<Base>().friendly && friendly)
            {
                currentTarget = bases[i].GetComponent<Base>().entrance;
                StartCoroutine(FirstCheckTimer());
                break;
            }
        }
    }

    public IEnumerator FirstCheckTimer()
    {
        yield return new WaitForEndOfFrame();
        RefreshPath(currentTarget.position);
    }

    public void Awake()
    {
        FirstCheck();
    }

    public void Update()
    {
        Move();
        TargetDetection();
        AttackCheck();
    }

    public void AttackCheck()
    {
        if(currentTarget.GetComponent<BaseUnit>() || currentTarget.GetComponent<BaseTower>())
        {
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                if (!attacking)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    public IEnumerator Attack()
    {
        attacking = true;
        if (ranged)
        {
            Debug.Log("He's a special one");
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
        if (path != null && path.Count > 0)
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
    }

    public void RefreshPath(Vector3 position)
    {
        path = GameObject.FindWithTag("Manager").GetComponent<PathFinding>().FindPath(transform.position, position);
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
                    int index
                    return targetsInRange[i].transform;
                }
            }
        }
        return null;
    }
}
