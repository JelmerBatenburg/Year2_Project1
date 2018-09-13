using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTowerPin : MonoBehaviour {

    public float rotationSpeed;
    public MeteorRain tower;
    public bool stopRotate;
    public float impactRange;

    public void Start()
    {
        StartCoroutine(TurnOnCollision());
    }

    public IEnumerator TurnOnCollision()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<Collider>().isTrigger = false;
    }

    public void FixedUpdate()
    {
        if (!stopRotate)
        {
            transform.parent.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        stopRotate = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        Destroy(transform.parent.gameObject, tower.fireAmount);
        StartCoroutine(ExtraShots());
    }
    public IEnumerator ExtraShots()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < tower.fireAmount; i++)
        {
            yield return new WaitForSeconds(tower.fireRate);
            StartCoroutine(tower.Fire(transform.position + new Vector3(Random.Range(-impactRange, impactRange), 0, Random.Range(-impactRange, impactRange)), MeteorRain.ShotType.laser, (i == tower.fireAmount - 1)? true : false));
        }
    }
}
