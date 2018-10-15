using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTowerPin : MonoBehaviour {

    public float rotationSpeed;
    public MeteorRain tower;
    public bool stopRotate;
    public float impactRange;
    public float currentRotate;

    public void Start()
    {
        StartCoroutine(TurnOnCollision());
        StartCoroutine(Stop());
    }

    public IEnumerator TurnOnCollision()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().isTrigger = false;
    }

    public IEnumerator Stop()
    {
        yield return new WaitForSeconds(1.92f);
        Check();
    }

    public void FixedUpdate()
    {
        if (!stopRotate)
        {
            transform.parent.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
            currentRotate += rotationSpeed * Time.deltaTime;
        }
    }

    public void Check()
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
            if (tower)
            {
                StartCoroutine(tower.Fire(transform.position + new Vector3(Random.Range(-impactRange, impactRange), 0, Random.Range(-impactRange, impactRange)), MeteorRain.ShotType.laser, (i == tower.fireAmount - 1) ? true : false));
            }
        }
    }
}
