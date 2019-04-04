using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    public Transform seeker, seeker1, seeker2, target;

    public List<Node> path = new List<Node>();
    public List<Node> path1 = new List<Node>();
    public List<Node> path2 = new List<Node>();

    bool once = true;

    public float speed;

    public int[] finalPath;
    public int[] finalPath1;
    public int[] finalPath2;

    int i = 0;
    int j = 0;
    int k = 0;

    Vector3 currentWaypoint;
    Vector3 currentWaypoint1;
    Vector3 currentWaypoint2;

    void Awake()
    {
        grid = GetComponent<Grid>();
        currentWaypoint = seeker.transform.position;
        currentWaypoint1 = seeker1.transform.position;
        currentWaypoint2 = seeker2.transform.position;
    }

    void Update()
    {
        MouseClick();
        CreateNodes();
        CreateNodes1();
        CreateNodes2();
        FollowPath();
        FollowPath1();
        FollowPath2();
    }

    void MouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 45f));
            target.transform.position = newPosition;
            if(target.transform.position.y != 0)
            {
                newPosition.y = 0;
                target.transform.position = newPosition;
            }
        }
    }

    void FollowPath()
    {

        if (grid.path != null)
        {
            if (seeker.transform.position == currentWaypoint)
            {
                if (i < grid.path.Count - 1)
                {
                    i++;
                    currentWaypoint = grid.path[i].worldPosition;
                }

                if (i >= grid.path.Count - 1)
                {
                    i = 0;
                }

                Debug.Log("Currentwaypoint: " + currentWaypoint);
            }
            seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, currentWaypoint, 0.1f);
        }

    }

    void FollowPath1()
    {
        if (grid.path1 != null)
        {
            if (seeker1.transform.position == currentWaypoint1)
            {
                if (j < grid.path1.Count - 1)
                {
                    j++;
                    currentWaypoint1 = grid.path1[j].worldPosition;
                }

                if (j >= grid.path1.Count - 1)
                {
                    j = 0;
                }

            }
            seeker1.transform.position = Vector3.MoveTowards(seeker1.transform.position, currentWaypoint1, 0.1f);
        }


    }

    void FollowPath2()
    {
        if (grid.path2 != null)
        {
            if (seeker2.transform.position == currentWaypoint2)
            {
                if (k < grid.path2.Count - 1)
                {
                    k++;
                    currentWaypoint2 = grid.path2[k].worldPosition;
                }

                if (k >= grid.path2.Count - 1)
                {
                    k = 0;
                }

            }
            seeker2.transform.position = Vector3.MoveTowards(seeker2.transform.position, currentWaypoint2, 0.1f);
        }


    }

    void CreateNodes()
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
                            Node test = grid.NodeFromWorldPoint(new Vector3(finalPath[i + 1], 0, Mathf.Abs((finalPath[i]) - 50)));
                            path.Add(test);
                        }
                    }

                    grid.path = path;

                    seeker.hasChanged = false;
                }
            }
            //yield return null;
        
  
    }

    void CreateNodes1()
    {
        finalPath1 = HelloRequester1.finalPath1;

        if (finalPath1[0] != 0)
        {
            if (seeker1.hasChanged || target.hasChanged)
            {
                path1.Clear();

                for (int i = 0; i < finalPath1.Length; i++)
                {
                    if (i % 2 == 0) // x-value
                    {
                        Node test = grid.NodeFromWorldPoint(new Vector3(finalPath1[i + 1], 0, Mathf.Abs((finalPath1[i]) - 50)));
                        path1.Add(test);
                    }
                }

                grid.path1 = path1;

                seeker1.hasChanged = false;
            }
        }
    }

    void CreateNodes2()
    {
        finalPath2 = HelloRequester2.finalPath2;

        if (finalPath2[0] != 0)
        {
            if (seeker2.hasChanged || target.hasChanged)
            {
                path2.Clear();

                for (int i = 0; i < finalPath2.Length; i++)
                {
                    if (i % 2 == 0) // x-value
                    {
                        Node test = grid.NodeFromWorldPoint(new Vector3(finalPath2[i + 1], 0, Mathf.Abs((finalPath2[i]) - 50)));
                        path2.Add(test);
                    }
                }

                grid.path2 = path2;

                seeker2.hasChanged = false;
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
