using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour {

    public Transform target;
    public float impactRange, speed;
    public float Distance() { return Vector3.Distance(transform.position, target.position); }
    public int damage;

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (target)
        {
            if (Distance() <= impactRange)
            {
                if (target.parent.GetComponent<Base>())
                {
                    target.parent.GetComponent<Base>().health -= damage;
                    target.parent.GetComponent<Base>().HealthCheck();
                }
                else if (target.GetComponent<BaseTower>())
                {
                    target.GetComponent<BaseTower>().health -= damage;
                    target.GetComponent<BaseTower>().HealthCheck();
                }
                Destroy(gameObject);
            }
        }
    }
}
