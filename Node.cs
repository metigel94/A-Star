using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;
    public bool difficult1;
    public bool difficult2;
    public bool difficult3;
    
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, bool _difficult1, bool _difficult2, bool _difficult3, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        difficult1 = _difficult1;
        difficult2 = _difficult2;
        difficult3 = _difficult3;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            if(difficult1 == true){
                hCost = 150;
            }
            if(difficult2 == true){
                hCost = 150;
            }
            if(difficult3 == true){
                hCost = 150;
            }
            return gCost + hCost;
        }
    }
}
