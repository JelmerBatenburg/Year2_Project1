using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    public bool friendly;
    public int health;
    public Transform entrance;

    public void HealthCheack()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
