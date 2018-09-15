using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour {

    public float tileSize;
    public AstarScanArea[] areas;
    [HideInInspector]
    public List<AstarScanInfo> scanLocations;
    public LayerMask groundMask;
    public LayerMask groundObstacles;
    public AStarGizmosList drawGizmos;
    public LayerMask obstacleMask;
    public Node[,] nodeList;

    public void Start()
    {
        for (int areaCount = 0; areaCount < areas.Length; areaCount++)
        {
            int areaAmountCheckWidth = Mathf.RoundToInt(areas[areaCount].scale.x / tileSize);
            int areaAmountCheckLength = Mathf.RoundToInt(areas[areaCount].scale.z / tileSize);
            for (int currentLength = 0; currentLength < areaAmountCheckLength; currentLength++)
            {
                for (int currentWidth = 0; currentWidth < areaAmountCheckWidth; currentWidth++)
                {
                    Vector3 position = areas[areaCount].position - new Vector3(areaAmountCheckWidth / 2 * tileSize, 0, areaAmountCheckLength / 2 * tileSize) + new Vector3(tileSize / 2, 0, tileSize / 2) + new Vector3(tileSize * currentWidth, 0, tileSize * currentLength);
                    if (Physics.OverlapBox(position, Vector3.one, Quaternion.identity, groundMask).Length > 0)
                    {
                        if (!Physics.CheckBox(position,Vector3.one * tileSize, Quaternion.identity, groundObstacles))
                        {
                            scanLocations.Add(new AstarScanInfo { worldLocation = position, gridX = currentWidth, gridY = currentLength });
                        }
                    }
                    else if ( currentWidth == 0 && currentLength == 00)
                    {
                        scanLocations.Add(new AstarScanInfo { worldLocation = position, gridX = currentWidth, gridY = currentLength });
                    }
                }
            }
        }
        RefreshGrid();
    }
    
    public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        Vector3 diffrence = a_WorldPosition - scanLocations[0].worldLocation;
        int x = Mathf.RoundToInt(diffrence.x / tileSize);
        int y = Mathf.RoundToInt(diffrence.z / tileSize);

        if(nodeList[y,x] != null)
        {
            return nodeList[y, x];
        }
        else
        {
            return null;
        }
    }
    
    public List<Node> GetNeighboringNodes(Node a_Node)
    {
        if(a_Node == null)
        {
            return null;
        }
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        bool right = false;
        bool left = false;
        bool up = false;
        bool down = false;

        //Right
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
        {
            if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
            {
                if(nodeList[yCheck,xCheck] != null)
                {
                    NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    right = true;
                }
            }
        }

        //Left
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
        {
            if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
            {
                if (nodeList[yCheck, xCheck] != null)
                {
                    NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    left = true;
                }
            }
        }

        //Up
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
        {
            if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
            {
                if (nodeList[yCheck, xCheck] != null)
                {
                    NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    up = true;
                }
            }
        }

        //Down
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
        {
            if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
            {
                if (nodeList[yCheck, xCheck] != null)
                {
                    NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    down = true;
                }
            }
        }

        //Right Up
        if (right && up)
        {
            xCheck = a_Node.gridX + 1;
            yCheck = a_Node.gridY + 1;
            if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
            {
                if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
                {
                    if (nodeList[yCheck, xCheck] != null)
                    {
                        NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    }
                }
            }
        }

        //Left Up
        if (left && up)
        {
            xCheck = a_Node.gridX - 1;
            yCheck = a_Node.gridY + 1;
            if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
            {
                if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
                {
                    if (nodeList[yCheck, xCheck] != null)
                    {
                        NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    }
                }
            }
        }

        //Right Down
        if (right && down)
        {
            xCheck = a_Node.gridX + 1;
            yCheck = a_Node.gridY - 1;
            if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
            {
                if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
                {
                    if (nodeList[yCheck, xCheck] != null)
                    {
                        NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    }
                }
            }
        }

        //Left Down
        if (left && down)
        {
            xCheck = a_Node.gridX - 1;
            yCheck = a_Node.gridY - 1;
            if (xCheck >= 0 && xCheck < scanLocations[scanLocations.Count - 1].gridX)
            {
                if (yCheck >= 0 && yCheck < scanLocations[scanLocations.Count - 1].gridY)
                {
                    if (nodeList[yCheck, xCheck] != null)
                    {
                        NeighboringNodes.Add(nodeList[yCheck, xCheck]);
                    }
                }
            }
        }
        return NeighboringNodes;
    }

    public void RefreshGrid()
    {
        List<int> xAmount = new List<int>();
        List<int> yAmount = new List<int>();
        for (int i = 0; i < scanLocations.Count; i++)
        {
            xAmount.Add(scanLocations[i].gridX);
            yAmount.Add(scanLocations[i].gridY);
        }
        nodeList = new Node[Mathf.Max(yAmount.ToArray()) + 1, Mathf.Max(xAmount.ToArray()) + 1];
        for (int i = 0; i < scanLocations.Count; i++)
        {
            bool wall = Physics.CheckBox(scanLocations[i].worldLocation, Vector3.one * tileSize, Quaternion.identity, obstacleMask)? false : true;
            nodeList[scanLocations[i].gridY, scanLocations[i].gridX] = new Node(wall, scanLocations[i].worldLocation, scanLocations[i].gridX, scanLocations[i].gridY);
        }
    }

    public IEnumerator Rescan()
    {
        yield return new WaitForEndOfFrame();
        RefreshGrid();
    }

    public void OnDrawGizmos()
    {
        if(areas.Length > 0 && drawGizmos.drawScanArea)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < areas.Length; i++)
            {
                Gizmos.DrawWireCube(areas[i].position, areas[i].scale);
            }
        }
        if (scanLocations.Count > 0 && drawGizmos.drawScannableTiles)
        {
            Gizmos.color = Color.black;
            for (int i = 0; i < scanLocations.Count; i++)
            {
                Gizmos.DrawWireCube(scanLocations[i].worldLocation, Vector3.one * tileSize);
            }
        }
        if (nodeList != null && drawGizmos.drawWalkableTiles)
        {
            foreach (Node node in nodeList)
            {
                if(node != null)
                {
                    Gizmos.color = (node.walkable) ? Color.white : Color.yellow;
                    Gizmos.DrawCube(node.position, Vector3.one * tileSize);
                }
            }
        }
    }
}

[System.Serializable]
public class AStarGizmosList
{
    public bool drawScanArea;
    public bool drawScannableTiles;
    public bool drawWalkableTiles;
}

[System.Serializable]
public class AstarScanArea
{
    public Vector3 position;
    public Vector3 scale;
}

[System.Serializable]
public class AstarScanInfo
{
    public Vector3 worldLocation;
    public int gridX;
    public int gridY;
    
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

    public Node(bool a_walkable, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        walkable = a_walkable;
        position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}