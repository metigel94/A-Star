using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;

// The implementation described in this class is identical to 'HelloRequester1' and 'HelloRequester2'.
// This class implements the 'RunAbleThread' Class.

public class HelloRequester : RunAbleThread
{
    public string messageToSend;
    public static int[] finalPath;

    protected override void Run()
    {
        ForceDotNet.Force();
        finalPath = new int[1000];

        // Create a new socket that binds to the port number for the first seeker character, as defined in Python.
        using (RequestSocket client = new RequestSocket())
        {
            // Port number for the first character defined in Python.
            client.Connect("tcp://localhost:5555");
            while(Running)
            {
                if (Send)
                {
                    // Here, the message string that is defined in 'HelloClient' is sent over the socket.
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
                        // Here, the string received from Python (the final path) is split to remove unnecessary characters.
                        // Furthermore, it is put into a static array, so the coordinates can be easily accessed in Pathfinding.cs 

                        string newMessage = message.Replace("(","");
                        string newNewMessage = newMessage.Replace(")", "");
                        string newNewNewMessage = newNewMessage.Replace(",", "");

                        

                        string[] finalStringPath = newNewNewMessage.Split();
                        finalPath = finalStringPath.Select(s => int.Parse(s)).ToArray();

                    }
                }       
            }
        }

        NetMQConfig.Cleanup();
    }
}