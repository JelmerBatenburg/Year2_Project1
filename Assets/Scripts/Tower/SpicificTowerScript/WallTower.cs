using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTower : BaseTower {

    public LayerMask wallmask;
    public List<GameObject> walls;
    public GameObject wall;

    private void Update()
    {
        InfoPopUp();
        if (placed)
        {
            Collider[] NearbyTowers = Physics.OverlapSphere(transform.position,6,wallmask);
            if(NearbyTowers.Length > 0)
            {
                for (int i = 0; i < NearbyTowers.Length; i++)
                {
                    if (NearbyTowers[i].GetComponent<WallTower>() && NearbyTowers[i].gameObject != gameObject)
                    {
                        GameObject tWall = Instantiate(wall, transform.position + (NearbyTowers[i].transform.position - transform.position)/2, wall.transform.rotation,transform.parent);
                        walls.Add(tWall);
                        NearbyTowers[i].GetComponent<WallTower>().walls.Add(tWall);
                        Vector3 checkRot = (NearbyTowers[i].transform.position - transform.position) / 2;
                        if (checkRot.x != 0)
                        {
                            tWall.transform.Rotate(0, 0, 90);
                        }
                    }
                }
            }
            placed = false;
        }
        for (int i = 0; i < walls.Count; i++)
        {
            if(!walls[i])
            {
                walls.RemoveAt(i);
            }
        }
        if (walls.Count == 2)
        {
            if (walls[0].transform.position + (walls[1].transform.position - walls[0].transform.position) / 2 == transform.position)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public override void HealthCheck()
    {
        if(health <= 0 && walls.Count > 0)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                Destroy(walls[i]);
            }
        }
        base.HealthCheck();
    }
}
