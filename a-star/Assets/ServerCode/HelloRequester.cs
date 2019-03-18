using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.


public class HelloRequester : RunAbleThread
{
    public string messageToSend;
    public List<Node> finalPath = new List<Node>();

    ///     Stop requesting when Running=false.
    protected override void Run()
    {
        ForceDotNet.Force(); 

        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            while(Running)
            {
                if (Send)
                {
                    //string message = client.ReceiveFrameString();
                    client.SendFrame(messageToSend);
                    

                    string message = null;
                    bool gotMessage = false;

                    while (Running)
                    {
                        gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                        if (gotMessage) break;
                    }
                    if (gotMessage)
                    {
                        //Debug.Log("Received " + message);

                        string newMessage = message.Replace("[(","");

                        newMessage.Replace("(", "");
                        newMessage.Replace(")", "");
                        newMessage.Replace(")]", "");

                        string[] finalPath = newMessage.Split();

                        foreach(string n in finalPath)
                        {
                            Debug.Log(n);
                        }


                    }
                }       
            }
        }

        NetMQConfig.Cleanup();
    }
}