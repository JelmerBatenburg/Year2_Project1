using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour {

    public float speed, damage, detectionRange;
    public Transform rotationPart;
    public LayerMask obstacleMask, enemyMask;
    public List<GameObject> damagedList = new List<GameObject>();
    public float damageRange;

    public void FixedUpdate()
    {
        rotationPart.Rotate(Time.deltaTime * new Vector3(speed, 0, 0) * 20);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, obstacleMask))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (!Physics.Raycast(transform.position, Vector3.down, out hit, detectionRange, obstacleMask))
        {
            if(Physics.Raycast(transform.position + Vector3.down, transform.forward, out hit, detectionRange, obstacleMask))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        EnemyDetection();
    }

    public void EnemyDetection()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, damageRange,enemyMask);
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (!damagedList.Contains(targetsInRange[i].gameObject))
            {
                damagedList.Add(targetsInRange[i].gameObject);
                targetsInRange[i].gameObject.GetComponent<BaseUnit>().Damage(damage);
            }
        }
    }
}
