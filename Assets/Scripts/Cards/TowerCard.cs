using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public Text nameInput;
    public Text descriptionInput;
    public Text costInput;
    public Image spriteInput;
    public TowerScriptableObject towerInput;

    public void Awake()
    {
        if (nameInput) { nameInput.text = towerInput.towerName; }
        if (descriptionInput) { descriptionInput.text = towerInput.description; }
        if (costInput) { costInput.text = "Cost = " + towerInput.buildCost.ToString(); }
        if (spriteInput) { spriteInput.sprite = towerInput.image; }
    }

    public void OnClick()
    {
        if(GameObject.FindWithTag("Manager").GetComponent<IngameCurrency>().currency >= towerInput.buildCost)
        {
            GameObject.FindWithTag("CamHolder").GetComponent<CameraViews>().towerGrid = true;
            TowerPlacement tower = GameObject.FindWithTag("TowerManager").GetComponent<TowerPlacement>();
            if (tower.active)
            {
                tower.found = false;
                Destroy(tower.tower);
            }
            tower.cost = towerInput.buildCost;
            GameObject tempTower = Instantiate(towerInput.tower, Vector3.one * 30, towerInput.tower.transform.rotation, tower.transform);
            tower.active = true;
            tower.tower = tempTower;
        }
    }
}
