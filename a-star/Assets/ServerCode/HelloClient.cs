using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;

// The implementation described in this class is identical to 'HelloClient1' and 'HelloClient2'.
public class HelloClient : MonoBehaviour
{
    public static HelloRequester _helloRequester;
    public static bool SendPack = true;
    public Quaternion joint;
    public Transform pos;
    string message;
    string positions;

    
    void Update()
    {
        if (SendPack)
        { 
            // Here, the x and y position of the first seeker and the target character and the grid converted into a string are stored in a variable.

             positions = Mathf.CeilToInt(GameObject.Find("SeekerCharacter").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("SeekerCharacter").transform.position.x) + ","
              + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.x) + ","
              + GameObject.Find("Grid").GetComponent<Grid>().convertedGrid;
            
            // The aforementioned variable is stored in 'messageToSend', so that the it can be sent in the 'HelloRequester' class.
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
        Debug.Log("HelloClientNormal");
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}