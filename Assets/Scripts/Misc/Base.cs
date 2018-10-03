using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    public bool friendly;
    public int health;
    public Transform entrance;

    public float spawnSpeed, waveSpeed;
    public int unitsPerWave, unitsAddedWave, currentWave;
    [Range(1,100)]
    public int rangePercentage, shieldPercentage;
    private bool sendingOut;
    public GameObject soldier, shieldman, ranged, deathScreen;

    public void Update()
    {
        if (!friendly && !sendingOut)
        {
            StartCoroutine(SendOutUnits());
        }
    }

    public void HealthCheck()
    {
        if(health <= 0)
        {
            if (friendly)
            {
                deathScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public IEnumerator SendOutUnits()
    {
        sendingOut = true;
        currentWave++;
        for (int i = 0; i < unitsPerWave + unitsAddedWave * currentWave; i++)
        {
            int percentage = Random.Range(1, 100);
            Instantiate((percentage < 100 - rangePercentage - shieldPercentage) ? soldier : (percentage < 100 - rangePercentage) ? shieldman : ranged, entrance.position, entrance.rotation);
            yield return new WaitForSeconds(spawnSpeed);
        }
        yield return new WaitForSeconds(waveSpeed);
        sendingOut = false;
    }
}