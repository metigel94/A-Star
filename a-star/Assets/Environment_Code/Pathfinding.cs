using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Pathfinding : MonoBehaviour
{
    // Declare and initialize variables

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
        // Store the grid and the position of each seeker in a variable.
        grid = GetComponent<Grid>();
        currentWaypoint = seeker.transform.position;
        currentWaypoint1 = seeker1.transform.position;
        currentWaypoint2 = seeker2.transform.position;
    }

    // All functions that are neccessary for the three seeker characters are called in the Upadte() function.
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

    // Target character moves to the position of the mouse-cursor, when the left mouse-button is clicked
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

    // This functions ensures that the first seeker-character moves along the final path
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

    // This functions ensures that the second seeker-character moves along the final path
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

    // This functions ensures that the third seeker-character moves along the final path
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

    // Add the final path received from python to the final path of the first seeker in Unity
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
    }

    // Add the final path received from python to the final path of the second seeker in Unity
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

    // Add the final path received from python to the final path of the third seeker in Unity
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
