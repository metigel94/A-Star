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


public class HelloRequester2 : RunAbleThread
{
    public string messageToSend2;
    public static int[] finalPath2;

    ///     Stop requesting when Running=false.
    protected override void Run()
    {
        ForceDotNet.Force();
        finalPath2 = new int[1000];

        using (RequestSocket client2 = new RequestSocket())
        {
            client2.Connect("tcp://localhost:5540");
            while (Running)
            {
                if (Send)
                {
                    client2.SendFrame(messageToSend2);


                    string message = null;
                    bool gotMessage = false;

                    while (Running)
                    {
                        gotMessage = client2.TryReceiveFrameString(out message); // this returns true if it's successful
                        if (gotMessage) break;
                    }
                    if (gotMessage)
                    {

                        string newMessage = message.Replace("(", "");
                        string newNewMessage = newMessage.Replace(")", "");
                        string newNewNewMessage = newNewMessage.Replace(",", "");



                        string[] finalStringPath = newNewNewMessage.Split();
                        finalPath2 = finalStringPath.Select(s => int.Parse(s)).ToArray();


                    }
                }
            }
        }

        NetMQConfig.Cleanup();
    }
}