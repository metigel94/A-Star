using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    public Transform seeker, target;

    public List<Node> path = new List<Node>();


    void Awake()
    {
        grid = GetComponent<Grid>();

    }

    void Update()
    {

        //Node test = grid.NodeFromWorldPoint(new Vector3(10, 3, 10));
        //Node test1 = grid.NodeFromWorldPoint(new Vector3(10, 3, 11));
        //Node test2 = grid.NodeFromWorldPoint(new Vector3(10, 3, 12));
        //Node test3 = grid.NodeFromWorldPoint(new Vector3(10, 3, 13));

        //path.Add(test);
        //path.Add(test1);
        //path.Add(test2);
        //path.Add(test3);



        grid.path = path;

        createNodes();

           
        // FindPath(seeker.position, target.position);
        // here you'd have to call the retrace path function with 
        // grid.path = OUR NEW STRING
    }


    void createNodes()
    {

        int[] finalPath = HelloClient._helloRequester.finalPath;

        
    }
}



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
