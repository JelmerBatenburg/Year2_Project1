using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
    public float scaler;
    public LayerMask enemyMask;
    public float speed;
    public int scale;

    public void Update()
    {
        if (scale == 1)
        {
            transform.localScale += Vector3.one * scaler * Time.deltaTime;
        }
        else if(scale == 2)
        {
            transform.localScale += -Vector3.one * scaler  * 2* Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Implose(0.2f));
    }

    public IEnumerator Implose(float time)
    {
        scale = 1;
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(time);
        Collider[] enemies = Physics.OverlapSphere(transform.position, transform.localScale.z, enemyMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        scale = 2;
        yield return new WaitForSeconds(time / 2);
        Destroy(gameObject);
    }
}
