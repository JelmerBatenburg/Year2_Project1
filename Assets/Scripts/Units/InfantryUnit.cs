using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryUnit : BaseUnit {

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
}
