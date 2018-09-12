using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour {

    public float speed, damage, detectionRange;
    public Transform rotationPart;

    public void FixedUpdate()
    {
        rotationPart.Rotate(Time.deltaTime * new Vector3(speed, 0, 0) * 20);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (!Physics.Raycast(transform.position, Vector3.down, out hit, detectionRange))
        {
            if(Physics.Raycast(transform.position + Vector3.down, transform.forward, out hit, detectionRange))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
