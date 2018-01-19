using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Security.Principal;

namespace DataTransfer
{
    public class PipeClient
    {
        private static bool isClose = false;
        private static bool isClosed = false;
        
        public static void CloseConnecting()
        {
            isClose = true;
            if (pipeClient.IsConnected)
            {
                while (!isClosed) ;
            }
            
        }
        private static NamedPipeClientStream pipeClient;
        public static void StartConnecting()
        {
            try
            {
                pipeClient = new NamedPipeClientStream(".", "k8mkZR5Apk",
                                PipeDirection.InOut, PipeOptions.None,
                                TokenImpersonationLevel.Impersonation);

                //Console.WriteLine("Connecting to server...\n");
                pipeClient.Connect();
                while (true)
                {
                    if (pipeClient.IsConnected)
                    {
                        StreamString ss = new StreamString(pipeClient);
                        
                        string dataReceive = ss.ReadString();
                        DataReceivedOperation(dataReceive);
                        System.Threading.Thread.Sleep(1);
                        //System.Diagnostics.Debug.WriteLine(dataReceive);
                        if (!isClose)
                        {
                            string dataToSend = LoadDataToSend();
                            ss.WriteString(dataToSend);
                            pipeClient.WaitForPipeDrain();
                            System.Threading.Thread.Sleep(1);
                        }
                        else
                        {
                            ss.WriteString("ENDSTREAMING");
                            pipeClient.WaitForPipeDrain();
                            string str = "";
                            while (str != "OK")
                            {
                                str = ss.ReadString();
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(1);
                }
                pipeClient.Close();
                isClosed = true;
            }
            catch (Exception)
            {
            }
        }
               


        public static void SetInput(string inputName, bool valueInput)
        {
            try
            {
                if (IOState.Inputs != null)
                {
                    IOState.Inputs[inputName] = valueInput;
                }
            }
            catch { }
        }

        public static bool GetOutput(string outputName)
        {
            try
            {
                if (IOState.Outputs != null)
                {
                    return IOState.Outputs[outputName];
                }
            }
            catch { }
            return false;
        }

        static bool IOInitialized = false;

        private static string LoadDataToSend()
        {
            string str = "";
            try
            {                
                if (IOState.Inputs != null)
                {
                    List<string> keys = new List<string>(IOState.Inputs.Keys);
                    for (int i = 0; i < keys.Count; i++)
                    {
                        str += keys[i] + "," + IOState.Inputs[keys[i]] + ",";
                    }
                    
                    str += "END";
                }
            }
            catch { }
            System.Diagnostics.Debug.WriteLine("Wysłany string:" + str);           
            return str;
        }

        private static void DataReceivedOperation(string message)
        {
            Console.WriteLine("Odebrany string:" + message);
            try
            {
                
                if (message != "")
                {
                    if (!IOInitialized)
                    {
                        IOState.Inputs = new Dictionary<string, bool>();
                        IOState.Outputs = new Dictionary<string, bool>();
                        string[] dataReceived = message.Split(',');
                        for (int i = 0; i < message.Length; i += 2)
                        {
                            if (dataReceived[i] == "END")
                            {
                                break;
                            }
                            string key = dataReceived[i];
                            bool value = Convert.ToBoolean(dataReceived[i + 1]);
                            if (key[0] == 'I')
                            {
                                IOState.Inputs.Add(key, value);
                            }
                            else if (key[0] == 'Q')
                            {
                                IOState.Outputs.Add(key, value);
                            }
                        }
                        IOInitialized = true;
                        List<string> keys = new List<string>(IOState.Inputs.Keys);                        
                    }
                    else
                    {

                        string[] dataReceived = message.Split(',');
                        for (int i = 0; i < message.Length; i += 2)
                        {
                            if (dataReceived[i] == "END")
                            {
                                break;
                            }
                            string key = dataReceived[i];
                            bool value = Convert.ToBoolean(dataReceived[i + 1]);
                            
                            if (IOState.Outputs.ContainsKey(key))
                            {
                                IOState.Outputs[key] = value;
                            }
                        }
                    }
                }                
            }
            catch (Exception)
            {
                //Debug.WriteLine(ex.Message);
            }
        }
    }
}
