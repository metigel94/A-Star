using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;

public class HelloClient : MonoBehaviour
{
    private HelloRequester _helloRequester;
    public bool SendPack = true;
    public Quaternion joint;
    public Transform pos;
    string message;
    string positions;

    
    void Update()
    {
        if (SendPack)
        { 


             positions = Mathf.RoundToInt(GameObject.Find("SeekerCharacter").transform.position.x) + "," + Mathf.RoundToInt(GameObject.Find("SeekerCharacter").transform.position.z) + ","
              + Mathf.RoundToInt(GameObject.Find("TargetCharacter").transform.position.x) + "," + Mathf.RoundToInt(GameObject.Find("TargetCharacter").transform.position.z) + ","
              + GameObject.Find("Grid").GetComponent<Grid>().convertedGrid;
            





            _helloRequester.messageToSend = positions;
           
            


            _helloRequester.Continue();

        } else if (!SendPack)
        {
            _helloRequester.Pause();
        }
    }

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}