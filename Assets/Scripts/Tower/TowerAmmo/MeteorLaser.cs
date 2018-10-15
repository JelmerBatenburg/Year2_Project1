using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorLaser : MonoBehaviour {

    public float rotationSpeed;
    public int damage;
    public float damageRange;
    public GameObject explosion;
    public LayerMask enemy;

    public IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(1.92f);
        Collider[] Targets = Physics.OverlapSphere(transform.position, damageRange,enemy);
        for (int i = 0; i < Targets.Length; i++)
        {
            if (Targets[i].gameObject.GetComponent<BaseUnit>())
            {
                Targets[i].gameObject.GetComponent<BaseUnit>().Damage(damage);
            }
        }
        GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(g, 3);
        Destroy(gameObject);
    }


    public void Start()
    {
        StartCoroutine(ExplosionTimer());
        StartCoroutine(TurnOnCollision());
    }

    public IEnumerator TurnOnCollision()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().isTrigger = false;
    }

    public void FixedUpdate()
    {
        transform.parent.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
