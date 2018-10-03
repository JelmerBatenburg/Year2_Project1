using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhalanxUnit : BaseUnit {

    [Range(1,100)]
    public float blockChance;

    public void Start()
    {
        FirstCheck();
    }

    public void Update()
    {
        TargetDetection();
        Attack();
        Move();
    }

    public override void Damage(float dmg)
    {
        int i = Random.Range(1, 100);
        if (i > blockChance)
        {
            health -= dmg;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
