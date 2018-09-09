using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour {

    public float health, damage, radius, fireRate;
    public bool placed;
    public TowerScriptObject towerInfo;
    public LayerMask targetMask;

    public GameObject[] TargetDetect(Vector3 position)
    {
        Collider[] targetsInRange= Physics.OverlapSphere(transform.position, radius,targetMask);
        List<GameObject> tempTarget = new List<GameObject>();
        RaycastHit hit;
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (Physics.Raycast(position, (targetsInRange[i].transform.position - position).normalized, out hit) && hit.transform.gameObject == targetsInRange[i].gameObject)
            {
                tempTarget.Add(targetsInRange[i].gameObject);
            }
        }
        return tempTarget.ToArray();
    }

    public void InfoPopUp()
    {
        if (Input.GetButtonDown("Fire1") && GameObject.FindWithTag("CamHolder").GetComponent<CameraViews>().towerGrid == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    GameObject.FindWithTag("IngameTowerDescription").GetComponent<TowerInfoUi>().SetTower(gameObject);
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
