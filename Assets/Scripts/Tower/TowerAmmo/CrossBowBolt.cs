using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowBolt : MonoBehaviour {

    public float speed;
    public bool launched;
    public int damage;

    public void Update()
    {
        StartCoroutine(StartTimer());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision c)
    {
        if (launched)
        {
            if (c.gameObject.GetComponent<BaseUnit>())
            {
                c.gameObject.GetComponent<BaseUnit>().Damage(damage);
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.1f);
        launched = true;
    }
}
