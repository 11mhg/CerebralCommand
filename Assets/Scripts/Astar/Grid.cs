using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    Node[,] grid;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        createGrid();
    }

    void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius,unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }

    public List<Node> GetNeighbours (Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x!=0 && y != 0)
                {
                    continue;
                }
                if (x==0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentx = (worldPos.x/ gridWorldSize.x);
        float percenty = (worldPos.y / gridWorldSize.y);
        percentx = Mathf.Clamp01(percentx);
        percenty = Mathf.Clamp01(percenty);

        int x =Mathf.RoundToInt( (gridSizeX - 1) * percentx);
        int y =Mathf.RoundToInt((gridSizeY - 1) * percenty);
        return grid[x, y];

    }
}
