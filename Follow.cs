using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public List<Node> foundPath;
    public GameObject seeker;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        foundPath = this.GetComponent<Pathfinding>().foundPath;
    }

    // Update is called once per frame
    void Update()
    {

     foundPath = this.GetComponent<Pathfinding>().foundPath;

 
          if(seeker.transform.position != foundPath[i].worldPosition){
              seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, foundPath[i].worldPosition, 0.1f);
          }
          else{
              i++;
          }

    }   
    }

