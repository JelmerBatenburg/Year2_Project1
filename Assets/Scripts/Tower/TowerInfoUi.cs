using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUi : MonoBehaviour {

    public GameObject selectedTower;
    public GameObject uiShow;
    public Text nameInput;
    public Text descriptionInput;
    public bool active;

    public void Update()
    {
        if (active)
        {
            if (!selectedTower)
            {
                uiShow.SetActive(false);
                active = false;
            }
            //transform.LookAt(Camera.main.transform.position);
            //transform.Rotate(0, 180, 0);
        }
    }

    public void SetTower(GameObject tower)
    {
        if (selectedTower)
        {
            selectedTower.GetComponent<BaseTower>().range.SetActive(false);
        }
        selectedTower = tower;
        //transform.position = tower.transform.position + Vector3.up * 20;
        uiShow.SetActive(true);
        nameInput.text = selectedTower.GetComponent<BaseTower>().towerInfo.name;
        descriptionInput.text = selectedTower.GetComponent<BaseTower>().towerInfo.description;
        active = true;
        selectedTower.GetComponent<BaseTower>().range.SetActive(true);
    }

    public void CloseButton()
    {
        uiShow.SetActive(false);
        active = false;
        selectedTower.GetComponent<BaseTower>().range.SetActive(false);
    }

    public void DestroyButton()
    {
        selectedTower.GetComponent<BaseTower>().health = 0;
        selectedTower.GetComponent<BaseTower>().HealthCheck();
        uiShow.SetActive(false);
        active = false;
    }
}
