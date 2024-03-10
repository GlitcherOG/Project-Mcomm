using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient
{
    public class EAClientManager
    {
        TcpClient MainClient = null;
        NetworkStream MainNS = null;

        TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        public void AssignListiners(TcpClient tcpClient)
        {
            NetworkStream tcpNS = tcpClient.GetStream();

            Console.WriteLine("Connection From: " + tcpClient.Client.RemoteEndPoint.ToString());

            //tcpClient.ReceiveTimeout = 20;

            //Read Incomming Message
            byte[] msg = new byte[1024];     //the messages arrive as byte array
            tcpNS.Read(msg, 0, msg.Length);

            string Test = ByteUtil.ReadString(msg, 0, 4);

            //Assign Listiner?

            //Close Connection

            //MainListen();
        }

        public void MainListen()
        {
            while (MainClient.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[1024];     //the messages arrive as byte array
                MainNS.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                Encoding encorder = new UTF8Encoding();
                if (msg[0] != 0)
                {
                    Console.WriteLine(encorder.GetString(msg)); //now , we write the message as string
                    Console.WriteLine(BitConverter.ToString(msg).Replace("-", ""));
                }
            }
        }
    }
}
