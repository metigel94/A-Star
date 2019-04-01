﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    public Transform seeker, target;

    public List<Node> path = new List<Node>();

    bool once = true;

    public float speed;

    public int[] finalPath;

    int i = 0;

    Vector3 currentWaypoint;



    void Awake()
    {
        grid = GetComponent<Grid>();
        currentWaypoint = seeker.transform.position;
    }

    void Update()
    {

        createNodes();
        //FollowPath();

    }

    void FollowPath()
    {

        if (grid.path != null)
        {
            if (seeker.transform.position == currentWaypoint)
            {
                if (i < grid.path.Count-1)
                {
                    i++;
                    currentWaypoint = grid.path[i].worldPosition;
                }

                if(i >= grid.path.Count-1)
                {
                    i = 0;
                }

                Debug.Log("Currentwaypoint: " + currentWaypoint);
            }
            seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, currentWaypoint, 0.1f);
        }

    }

    void createNodes()
    {
        finalPath = HelloRequester.finalPath;

        if (finalPath[0] != 0)
        {
            if (seeker.hasChanged || target.hasChanged)
            {
                path.Clear();

                for (int i = 0; i < finalPath.Length; i++)
                {
                    if (i % 2 == 0) // x-value
                    {
                        Node test = grid.NodeFromWorldPoint(new Vector3(finalPath[i + 1], 0 , Mathf.Abs((finalPath[i]) - 50)));
                        path.Add(test);
                    }
                }

                grid.path = path;

                // FollowPath(finalPath, grid.path);

                seeker.hasChanged = false;
                target.hasChanged = false;
            }
        }
    }
}

//    IEnumerator FollowPath(int[] finalPath, List<Node> path)
//    {
//        Vector3 currentWaypoint = new Vector3(finalPath[0], 0, finalPath[1]);

//        while(true)
//        {
//            if(seeker.transform.position == currentWaypoint)
//            {
//                targetIndex++;
//                if(targetIndex >= path.Count)
//                {
//                    yield break;
//                }
//                if(targetIndex % 2 == 0)
//                {
//                    currentWaypoint = new Vector3(finalPath[targetIndex], 0, finalPath[targetIndex+1]);
//                }
//            }

//            seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, currentWaypoint, 0.05f);
//            yield return null;
//        }
//    }
//}



//    void FindPath(Vector3 startPos, Vector3 targetPos)
//    {
//        Node startNode = grid.NodeFromWorldPoint(startPos);
//        Node targetNode = grid.NodeFromWorldPoint(targetPos);

//        List<Node> openSet = new List<Node>();
//        HashSet<Node> closedSet = new HashSet<Node>();
//        openSet.Add(startNode);

//        while (openSet.Count > 0)
//        {
//            Node currentNode = openSet[0];
//            for (int i = 1; i < openSet.Count; i++)
//            {
//                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
//                {
//                    if (openSet[i].hCost < currentNode.hCost)
//                    {
//                        currentNode = openSet[i];
//                    }
//                }
//            }

//            openSet.Remove(currentNode);
//            closedSet.Add(currentNode);

//            if (currentNode == targetNode)
//            {
//                RetracePath(startNode, targetNode);
//                return;
//            }

//            foreach (Node neighbor in grid.GetNeighbors(currentNode))
//            {
//                if (!neighbor.walkable || closedSet.Contains(neighbor))
//                {
//                    continue;
//                }
//                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
//                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
//                {
//                    neighbor.gCost = newMovementCostToNeighbor;
//                    neighbor.hCost = GetDistance(neighbor, targetNode);
//                    neighbor.parent = currentNode;

//                    if (!openSet.Contains(neighbor))
//                        openSet.Add(neighbor);
//                }
//            }
//        }
//    }


//    void RetracePath(Node startNode, Node endNode)
//    {
//        List<Node> path = new List<Node>();
//        Node currentNode = endNode;
//        while (currentNode != startNode)
//        {
//            path.Add(currentNode);
//            currentNode = currentNode.parent;
//        }
//        path.Reverse();
//        grid.path = path;

//    }

//    int GetDistance(Node nodeA, Node nodeB)
//    {
//        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
//        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);


//        if (dstX > dstY)
//        {
//            return 14 * dstY + 10 * (dstX - dstY);
//        }

//        return 14 * dstX + 10 * (dstY - dstX);
//    }
//}
