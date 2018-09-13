using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorLaser : MonoBehaviour {

    public float rotationSpeed;

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
        transform.parent.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.parent.gameObject);
    }
}
