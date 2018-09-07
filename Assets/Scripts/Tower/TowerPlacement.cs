using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour {

    public GameObject tower;
    public TowerGrid grid;

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            List<float> distances = new List<float>();
            for (int i = 0; i < grid.tiles.Count; i++)
            {
                distances.Add(Vector3.Distance(grid.tiles[i].location, hit.point));
            }

            for (int i = 0; i < distances.Count; i++)
            {
                if(distances[i] == Mathf.Min(distances.ToArray()))
                {
                    tower.transform.position = grid.tiles[i].location;
                    break;
                }
            }

            
        }
    }
}
