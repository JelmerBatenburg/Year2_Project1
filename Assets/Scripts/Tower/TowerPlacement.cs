﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour {

    public GameObject tower;
    public TowerGrid grid;
    public bool active;
    public bool found;
    public int cost;
    public GameObject towerPlacementInfo;

    public void Update()
    {
        if (active)
        {
            towerPlacementInfo.SetActive(true);
            towerPlacementInfo.transform.position = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                List<float> distances = new List<float>();
                for (int i = 0; i < grid.tiles.Count; i++)
                {
                    if (!grid.tiles[i].taken && grid.tiles[i].inRange)
                    {
                        distances.Add(Vector3.Distance(grid.tiles[i].location, hit.point));
                    }
                    else
                    {
                        distances.Add(999);
                    }
                }

                for (int i = 0; i < distances.Count; i++)
                {
                    if (distances[i] == Mathf.Min(distances.ToArray()))
                    {
                        tower.transform.position = grid.tiles[i].location;
                        found = true;
                        break;
                    }
                }

                if (Input.GetButtonDown("Fire1") && found)
                {
                    towerPlacementInfo.SetActive(false);
                    IngameCurrency currency = GameObject.FindWithTag("Manager").GetComponent<IngameCurrency>();
                    currency.currency -= tower.GetComponent<BaseTower>().towerInfo.buildCost;
                    tower.GetComponent<Collider>().enabled = true;
                    active = false;
                    tower.GetComponent<BaseTower>().placed = true;
                    tower.GetComponent<BaseTower>().range.SetActive(false);
                    tower = null;
                    found = false;
                    currency.CurrencyChange();
                    GameObject.FindWithTag("CamHolder").GetComponent<CameraViews>().towerGrid = false;
                    for (int i = 0; i < distances.Count; i++)
                    {
                        if (distances[i] == Mathf.Min(distances.ToArray()))
                        {
                            grid.tiles[i].taken = true;
                            break;
                        }
                    }
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    towerPlacementInfo.SetActive(false);
                    active = false;
                    found = false;
                    Destroy(tower);
                    GameObject.FindWithTag("CamHolder").GetComponent<CameraViews>().towerGrid = false;
                }
            }
        }
    }
}
