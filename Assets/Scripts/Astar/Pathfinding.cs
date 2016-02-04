using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {


    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

	public List<Node> FindPath(Vector3 startpos, Vector3 targetpos)
    {
        Node startNode = grid.NodeFromWorldPoint(startpos);
        Node targetNode = grid.NodeFromWorldPoint(targetpos);
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDist(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDist(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        return new List<Node>();
    }

    List<Node> RetracePath(Node start, Node end)
    {
        List<Node> Path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            Path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Path.Reverse();

        return Path;
        
    }

    int GetDist(Node A, Node B)
    {
        int dstx = Mathf.Abs(A.gridX - B.gridX);
        int dsty = Mathf.Abs(A.gridY - B.gridY);
        if (dstx > dsty)
        {
            return 14 * dsty + 10 * (dstx - dsty);
        }
        return 14 * dstx + 10 * (dsty - dstx);
    }
}
