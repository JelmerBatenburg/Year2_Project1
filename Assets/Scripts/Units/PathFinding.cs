using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    public AStarGrid grid;

    public Transform from, to;
    public bool check;
    public List<Node> path;

    public void Update()
    {
        if (check)
        {
            check = false;
            FindPath(from.position, to.position);
        }
    }

    public List<Node> FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);
        while (!TargetNode.walkable)
        {
            Vector3 dir = (TargetNode.position - StartNode.position).normalized;
            TargetNode = grid.nodeList[TargetNode.gridY - Mathf.RoundToInt(dir.z), TargetNode.gridX - Mathf.RoundToInt(dir.x)];
        }

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if(CurrentNode == TargetNode)
            {
                return GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighboreNode in grid.GetNeighboringNodes(CurrentNode))
            {
                
                if (!NeighboreNode.walkable || ClosedList.Contains(NeighboreNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetManHattenDistance(CurrentNode, NeighboreNode);

                if (MoveCost < NeighboreNode.gCost || !OpenList.Contains(NeighboreNode))
                {
                    NeighboreNode.gCost = MoveCost;
                    NeighboreNode.hCost = GetManHattenDistance(NeighboreNode, TargetNode);
                    NeighboreNode.parent = CurrentNode;

                    if (!OpenList.Contains(NeighboreNode))
                    {
                        OpenList.Add(NeighboreNode);
                    }
                }
            }
        }
        return null;
    }

    int GetManHattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }

    List<Node> GetFinalPath(Node a_StartNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node currentNode = a_EndNode;

        while (currentNode != a_StartNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        FinalPath.Reverse();
        path = FinalPath;
        return FinalPath;
    }

    public void OnDrawGizmos()
    {
        if(path != null && path.Count > 0)
        {
            foreach (Node node in path)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(node.position, Vector3.one);
            }
        }
    }
}
