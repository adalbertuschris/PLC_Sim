using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Threading;
using System.IO;

namespace DataTransfer
{
    

    public class PipeServer
    {        
        public delegate void DelegateReceive(string Reply);
        public delegate string DelegateSend();
        public delegate void DelegateConnectionClose();

        public static event DelegateReceive DataReceivedOperation;
        public static event DelegateSend LoadDataToSend;
        public static event DelegateConnectionClose ConnectionClose;

        

        public static void StartListening()
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("k8mkZR5Apk", PipeDirection.InOut, 1);
            try
            {
            // Wait for a client to connect
            pipeServer.WaitForConnection();

            
                StreamString ss = new StreamString(pipeServer);
                
                while (true)
                {
                    if (pipeServer.IsConnected)
                    {
                        
                        //try
                        //{
                            string dataToSend = LoadDataToSend();
                            ss.WriteString(dataToSend);
                            System.Threading.Thread.Sleep(1);
                        //}
                        //catch { }
                        string dataReceive = ss.ReadString();
                        if (dataReceive == "ENDSTREAMING")
                        {                            
                            ss.WriteString("OK");
                            pipeServer.WaitForPipeDrain();
                            System.Threading.Thread.Sleep(1);
                            ConnectionClose();      
                            break;
                        }
                        else
                        {
                            DataReceivedOperation(dataReceive);
                            System.Threading.Thread.Sleep(1);
                        }                       
                    }
                    else
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(1);
                }
            }            
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
        }
    }
}
