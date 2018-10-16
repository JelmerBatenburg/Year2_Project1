using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    public bool friendly;
    public int health;
    public Transform entrance;

    public float spawnSpeed, waveSpeed;
    public int unitsPerWave, unitsAddedWave, currentWave, maxWaves;
    [Range(1,100)]
    public int rangePercentage, shieldPercentage;
    private bool sendingOut;
    public GameObject soldier, shieldman, ranged, deathScreen;
    public Animator newWaveManager;
    public Text waveInput;

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
        newWaveManager.SetTrigger("NewWave");
        sendingOut = true;
        currentWave++;
        if(currentWave < maxWaves)
        {
            waveInput.text = currentWave.ToString() + " / " + maxWaves.ToString();
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
}