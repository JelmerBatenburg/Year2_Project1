using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour {

    public float health, damage, radius;
    public bool placed;
    public TowerScriptObject towerInfo;

    public void InfoPopUp()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    health = 0;
                    HealthCheck();
                }
            }
        }
    }

    public virtual void HealthCheck()
    {
        if (health <= 0)
        {
            TowerGrid grid = GameObject.FindWithTag("TowerManager").GetComponent<TowerGrid>();
            for (int i = 0; i < grid.tiles.Count; i++)
            {
                if(grid.tiles[i].location == transform.position)
                {
                    grid.tiles[i].taken = false;
                }
            }
            Destroy(gameObject);
        }
    }
}
