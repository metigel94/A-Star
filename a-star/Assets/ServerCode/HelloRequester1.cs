using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;


///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.


public class HelloRequester1 : RunAbleThread
{
    public string messageToSend1;
    public static int[] finalPath1;

    ///     Stop requesting when Running=false.
    protected override void Run()
    {
        ForceDotNet.Force();
        finalPath1 = new int[49];

        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5550");
            while (Running)
            {
                if (Send)
                {
                    //string message = client.ReceiveFrameString();
                    client.SendFrame(messageToSend1);


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

                        string newMessage = message.Replace("(", "");
                        string newNewMessage = newMessage.Replace(")", "");
                        string newNewNewMessage = newNewMessage.Replace(",", "");



                        string[] finalStringPath = newNewNewMessage.Split();
                        finalPath1 = finalStringPath.Select(s => int.Parse(s)).ToArray();


                    }
                }
            }
        }

        NetMQConfig.Cleanup();
    }
}