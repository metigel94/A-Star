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
 
    
    void Update()
    {
        if (SendPack)
        {
            string positions = GameObject.Find("SeekerCharacter").transform.position.ToString() + "   " + GameObject.Find("TargetCharacter").transform.position.ToString();

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