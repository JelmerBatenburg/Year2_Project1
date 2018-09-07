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
    public TowerScriptObject towerInput;

    public void OnClick()
    {
        GameObject.FindWithTag("CamHolder").GetComponent<CameraViews>().towerGrid = true;
        TowerPlacement tower = GameObject.FindWithTag("TowerManager").GetComponent<TowerPlacement>();
        if (tower.active)
        {
            tower.found = false;
            Destroy(tower.tower);
        }
        tower.cost = towerInput.buildCost;
        GameObject tempTower = Instantiate(towerInput.tower, Vector3.one * 30, towerInput.tower.transform.rotation);
        tower.active = true;
        tower.tower = tempTower;
    }
}


[CreateAssetMenu(fileName = "TowerScriptableObject",menuName = "TowerScriptableObject")]
public class TowerScriptObject : ScriptableObject {

    public string towerName;
    public string description;
    public int buildCost;
    public Sprite image;
    public GameObject tower;
}
