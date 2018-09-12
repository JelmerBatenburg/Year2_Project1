using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowBolt : MonoBehaviour {

    public float speed;
    public bool launched;

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision c)
    {
        if (launched)
        {
            Destroy(gameObject);
        }
    }

}
