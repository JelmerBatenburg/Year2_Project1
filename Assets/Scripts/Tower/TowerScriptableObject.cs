using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerScriptableObject", menuName = "TowerScriptableObject")]
public class TowerScriptObject : ScriptableObject
{

    public string towerName;
    public string description;
    public int buildCost;
    public Sprite image;
    public GameObject tower;
}
