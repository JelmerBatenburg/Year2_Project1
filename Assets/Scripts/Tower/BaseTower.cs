using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour {

    public float health, damage, radius, fireRate;
    public bool placed;
    public TowerScriptableObject towerInfo;
    public LayerMask targetMask, raycastmask;

    public GameObject TargetDetect(Vector3 position, bool raycast)
    {
        Collider[] targetsInRange= Physics.OverlapSphere(transform.position, radius,targetMask);
        List<GameObject> tempTarget = new List<GameObject>();
        RaycastHit hit;
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (raycast)
            {
                if (Physics.Raycast(position, (targetsInRange[i].transform.position - position).normalized, out hit,Mathf.Infinity,raycastmask) && hit.transform.gameObject == targetsInRange[i].gameObject)
                {
                    tempTarget.Add(targetsInRange[i].gameObject);
                }
            }
            else
            {
                tempTarget.Add(targetsInRange[i].gameObject);
            }
        }
        GameObject returnEnemy = null;

        if(tempTarget.Count > 1)
        {
            List<float> distances = new List<float>();
            for (int i = 0; i < tempTarget.Count; i++)
            {
                distances.Add(Vector3.Distance(tempTarget[i].transform.position, position));
            }

            for (int i = 0; i < distances.Count; i++)
            {
                if(distances[i] == Mathf.Min(distances.ToArray()))
                {
                    returnEnemy = tempTarget[i];
                }
            }
        }
        else if(tempTarget.Count > 0)
        {
            returnEnemy = tempTarget[0];
        }

        if (returnEnemy != null)
        {
            return returnEnemy;
        }
        else
        {
            return null;
        }
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
