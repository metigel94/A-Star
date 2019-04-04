using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;

public class HelloClient2 : MonoBehaviour
{
    public static HelloRequester2 _helloRequester;
    public static bool SendPack = true;
    public Quaternion joint;
    public Transform pos;
    string message;
    string positions;


    void Update()
    {
        if (SendPack)
        {


            positions = Mathf.CeilToInt(GameObject.Find("SeekerCharacter2").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("SeekerCharacter2").transform.position.x) + ","
             + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.z) + "," + Mathf.CeilToInt(GameObject.Find("TargetCharacter").transform.position.x) + ","
             + GameObject.Find("Grid").GetComponent<Grid>().convertedGrid;

            _helloRequester.messageToSend2 = positions;


            _helloRequester.Continue();

        }
        else if (!SendPack)
        {
            _helloRequester.Pause();
        }
    }

    private void Start()
    {
        _helloRequester = new HelloRequester2();
        Debug.Log("HelloClient2");
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}