using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    Node[,] grid;
    public LayerMask unwalkableMask;
    public LayerMask grassMask;
    public LayerMask muddMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float height;
    public string convertedGrid = "";

    float nodeDiameter;
    public int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position *
            gridWorldSize.x / 2  * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right *
                    (x * nodeDiameter + nodeRadius) + Vector3.forward *
                    (y * nodeDiameter + nodeRadius);

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                bool grass = (Physics.CheckSphere(worldPoint, nodeRadius, grassMask));
                bool mudd = (Physics.CheckSphere(worldPoint, nodeRadius, muddMask));

                grid[x,y] = new Node(walkable, grass, mudd, worldPoint, x, y);

               
            }
        }


        for(int y = gridSizeY-1; y > 0; y--)
        {
            for(int x = 0; x < gridSizeX; x++)
            {

                Vector3 convertedWorldPoint = worldBottomLeft + Vector3.right *
                   (x * nodeDiameter + nodeRadius) + Vector3.forward *
                   (y * nodeDiameter + nodeRadius);

                bool convertedWalkable = !(Physics.CheckSphere(convertedWorldPoint, nodeRadius, unwalkableMask));
                bool convertedGrass = (Physics.CheckSphere(convertedWorldPoint, nodeRadius, grassMask));
                bool convertedMudd = (Physics.CheckSphere(convertedWorldPoint, nodeRadius, muddMask));

                if (convertedWalkable && convertedGrass == false && convertedMudd == false) // Normal Walking terrain - Indicated by a 0
                {
                    convertedGrid += "0";
                }
                else if (convertedGrass && convertedWalkable && convertedMudd == false) // Grass Terrain - Indicated by a 1
                {
                    convertedGrid += "1";
                }
                else if (convertedMudd && convertedWalkable && convertedGrass == false) // Muddy Terrain - Indicated by a 2 
                {
                    convertedGrid += "2";
                }
                else if(convertedWalkable == false) // Non-walkable Terrain - Indicated by a 3
                {
                    convertedGrid += "3";
                }

            }
        }
       
        Debug.Log(grid.Length);
    }


    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        //float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        //float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        //percentX = Mathf.Clamp01(percentX);
        //percentY = Mathf.Clamp01(percentY);

        //int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        //int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        int x = Mathf.RoundToInt(worldPosition.x / (nodeRadius * 2)) -1;
        int y = Mathf.RoundToInt(worldPosition.z / (nodeRadius * 2)) -1;

        return grid[x, y];
    }


    public List<Node> path;
    public List<Node> path1;
    void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position + Vector3.right * gridWorldSize.x / 2 + Vector3.forward * gridWorldSize.y / 2, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node n in grid)
            {
                if(n.walkable && n.grass == false && n.mudd == false)
                {
                    Gizmos.color = Color.white;
                }

                else if(n.grass && n.walkable && n.mudd == false)
                {
                    Gizmos.color = Color.green;
                }

                else if(n.mudd && n.grass == false && n.walkable)
                {
                    Gizmos.color = Color.blue;
                }

                else if (n.walkable == false && n.grass == false && n.mudd == false)
                {
                    Gizmos.color = Color.red;
                }

                if(path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }

                if(path1 != null)
                {
                    if (path1.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.worldPosition, new Vector3(1, height, 1) * (nodeDiameter - .1f));
            }
        }
    }
}
