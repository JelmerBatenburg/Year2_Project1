using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour {

    public GroundScanArea[] scannableArea;
    public bool drawGizmos;
    public Color gizmoColor;
    public int towerSize;
    public List<TowerTile> tiles;
    public LayerMask groundMask;
    public GameObject tileShowPrefab;
    public float tileShowGap;

    

    public void Start()
    {
        for (int areaCount = 0; areaCount < scannableArea.Length; areaCount++)
        {
            int areaAmountCheckWidth = Mathf.RoundToInt(scannableArea[areaCount].size.x / towerSize);
            int areaAmountCheckLength = Mathf.RoundToInt(scannableArea[areaCount].size.z / towerSize);
            for (int currentLength = 0; currentLength < areaAmountCheckLength; currentLength++)
            {
                for (int currentWidth = 0; currentWidth < areaAmountCheckWidth; currentWidth++)
                {
                    Vector3 position = scannableArea[areaCount].position - new Vector3(areaAmountCheckWidth/2 * towerSize, 0, areaAmountCheckLength/2* towerSize) + new Vector3(towerSize/2,0,towerSize/2) + new Vector3(towerSize * currentWidth,0,towerSize * currentLength);
                    if (Physics.OverlapBox(position,Vector3.one,Quaternion.identity,groundMask).Length > 0)
                    {
                        GameObject obj = Instantiate(tileShowPrefab, position, Quaternion.identity,transform);
                        obj.transform.localScale = new Vector3(towerSize - tileShowGap, 0.05f, towerSize-tileShowGap);
                        TowerTile tile = new TowerTile
                        {
                            taken = false,
                            location = position
                        };
                        tiles.Add(tile);
                    }
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        if(scannableArea.Length != 0 && drawGizmos)
        {
            for (int i = 0; i < scannableArea.Length; i++)
            {
                Gizmos.DrawWireCube(scannableArea[i].position, scannableArea[i].size);
            }
        }
    }
}

[System.Serializable]
public class TowerTile
{
    public bool taken;
    public Vector3 location;
}

[System.Serializable]
public class GroundScanArea
{
    public Vector3 size;
    public Vector3 position;
}
