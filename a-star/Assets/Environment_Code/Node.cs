using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Public Node class that contains all the necessary attributes for a Node, including type of tile,
// position in world coordinates and the dimensions of the grid.
public class Node {

    public bool walkable;
    public bool grass;
    public bool mudd;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, bool _grass, bool _mudd, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        grass = _grass;
        mudd = _mudd;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
