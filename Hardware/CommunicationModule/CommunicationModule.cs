using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public delegate void CommunicationDelegate(object sender, EventArgs e);
    public class CommunicationModule
    {
        public static AsynchronousSocketListener Server = new AsynchronousSocketListener();
        public CommunicationModule()
        {
            //Server = 
             //Server.DataReceived += DataReceivedCallback; 
        }
        //public static event CommunicationDelegate Communication;
        public static void Run()
        {            
            Server.StartListening();
        }
        public static void Stop()
        {
            Server.Disconnect();
        }
    }
}
