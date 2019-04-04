using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;

public class HelloClient1 : MonoBehaviour
{
    public static HelloRequester1 _helloRequester;
    public static bool SendPack = true;
    public Quaternion joint;
    public Transform pos;
    string message;
    string positions;


    void Update()
    {
        if (SendPack)
        {


            positions = Mathf.CeilToInt(GameObject.Find("SeekerCharacter1").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("SeekerCharacter1").transform.position.x) + ","
             + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.x) + ","
             + GameObject.Find("Grid").GetComponent<Grid>().convertedGrid;

            _helloRequester.messageToSend1 = positions;


            _helloRequester.Continue();

        }
        else if (!SendPack)
        {
            _helloRequester.Pause();
        }
    }

    private void Start()
    {
        _helloRequester = new HelloRequester1();
        Debug.Log("HelloClient1");
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}