using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public List<Node> foundPath;
    public GameObject seeker;
    public Vector3 currentWaypoint;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        foundPath = this.GetComponent<Pathfinding>().grid.path;
        currentWaypoint = foundPath[i].worldPosition;
    }

    // Update is called once per frame
    void Update()
    {

     foundPath = this.GetComponent<Pathfinding>().grid.path;
    //Debug.Log(foundPath[4].worldPosition);
      while(true)
         if(seeker.transform.position == currentWaypoint){
            i++;
            if(i >= foundPath.Count){
                break;
            }
            currentWaypoint = foundPath[i].worldPosition;
         }
           seeker.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, 0.1f);
         
             
            //Debug.Log(currentWaypoint);
         }
        
     }

      /* 
          if(seeker.transform.position != foundPath[i].worldPosition){
              seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, foundPath[i].worldPosition, 0.1f);
          }
             if(seeker.transform.position == foundPath[i].worldPosition){
              i++;
          }

    }   
    }
*/