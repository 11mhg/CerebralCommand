using UnityEngine;
using System.Collections;

public class Node: IHeapItem<Node> {
    int heapIndex;
    public bool walkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;

    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost{
        get { return gCost + hCost; }
    }

    public Node(bool _walkable, Vector3 _worldpos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPos = _worldpos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }

        return -compare;
    }
	
}
