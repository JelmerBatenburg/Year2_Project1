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

    public void Start()
    {
        for (int areaCount = 0; areaCount < scannableArea.Length; areaCount++)
        {
            int areaAmountCheckWidth = Mathf.RoundToInt(scannableArea[areaCount].size.x / towerSize);
            int areaAmountCheckLength = Mathf.RoundToInt(scannableArea[areaCount].size.z / towerSize);
            Debug.Log("WidthAmount = " + areaAmountCheckWidth + " / LengthAmount = " + areaAmountCheckLength);

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
