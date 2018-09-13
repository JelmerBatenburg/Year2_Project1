using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour {

    public int tileSize;
    public AstarScanArea[] areas;

    public void OnDrawGizmos()
    {
        if(areas.Length > 0)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < areas.Length; i++)
            {
                Gizmos.DrawWireCube(areas[i].position, areas[i].scale);
            }
        }
    }
}

[System.Serializable]
public class AstarScanArea
{
    public Vector3 position;
    public Vector3 scale;
}

public class Node
{
    public int gridX;
    public int gridY;

    public bool walkable;
    public Vector3 position;

    public Node parent;

    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_walkable,Vector3 a_Pos,int a_gridX,int a_gridY)
    {
        walkable = a_walkable;
        position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
