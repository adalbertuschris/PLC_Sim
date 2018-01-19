using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Packets;
using System.Diagnostics;
//using ;

namespace S7_300_Engine
{
    public class Engine
    {
        public static bool firstPartDataReceived = false;
        public static void Initialize()
        {
            Packets.S7PacketGenerator.Downloaded += Downloads;
            Hardware.CPU.DiagnosticData.Diagnostic += Packets.S7PacketGenerator.CreateDiagData;
        }

        public static void SetPLCInfo(string shortDesignation, string orderNumber)
        {
            Packets.PLCInfo.ShortDesignation = shortDesignation;
            Packets.PLCInfo.OrderNumber = orderNumber;
        }

        public static void Downloads(byte[] data)
        {
            List<byte[]> networks = CreateNetworkList(data);

            string hex = BitConverter.ToString(data);
            
            hex = hex.Replace("-", "|");

            Debug.WriteLine("Wgrane dane: " + hex);
			
            
            Hardware.PLC.WriteProgramToMemory(networks);
        }

        public static void client_DataReceived(object sender, Hardware.CommunicationModule.DataReceivedEventArgs e)
        {
            Assistant assistant = new Assistant();

            Socket client = sender as Socket;

            assistant.SomethingToSend = false;
            assistant.SomethingToReceive = true;
            Hardware.CommunicationModule.DataReceivedEventArgs courier = e as Hardware.CommunicationModule.DataReceivedEventArgs;

            byte[] dataReceived = courier.RcvInfo.Data;
            int dataLength = courier.RcvInfo.Count;
            if (dataLength != 7)
            {
                assistant.SomethingToSend = S7Packet.CheckCode(dataReceived, assistant);
                assistant.SomethingToReceive = !(S7PacketGenerator.requestDiagData || S7PacketGenerator.requestDownload);
            }
            else
            {
                if (S7PacketGenerator.requestDownload)
                {
                    assistant.SomethingToSend = true;
                    assistant.DataToSend = S7PacketGenerator.DownloadBlock().ToArray();
                    S7PacketGenerator.requestDownload = false;
                }
                else if (S7PacketGenerator.requestDiagData)
                {
                    assistant.SomethingToSend = true;
                    assistant.DataToSend = S7PacketGenerator.PushRequestDiagData().ToArray();
                    S7PacketGenerator.requestDiagData = false;
                    S7PacketGenerator.lockedAccessToDiagData = false;
                }
                else
                {
                    assistant.SomethingToSend = false;
                }
            }

            bool somethingToSend = assistant.SomethingToSend;
            bool somethingToReceive = assistant.SomethingToReceive;
            if (somethingToSend)
            {
                string str = "";
                for (int i = 0; i < assistant.DataToSend.Length; i++)
                {
                    str += assistant.DataToSend[i] + ",";
                }
                System.Diagnostics.Debug.WriteLine(str);
                courier.FeedBack.SetFeedback(new Hardware.CommunicationModule.SendInfo(somethingToSend, somethingToReceive, assistant.DataToSend));
            }
            else
            {
                courier.FeedBack.SetFeedback(new Hardware.CommunicationModule.SendInfo(somethingToSend, somethingToReceive));
            }
        }

        public static List<byte[]> CreateNetworkList(byte[] data)
        {
            List<byte[]> networks;

            int count = data[60] - 2;
            System.Diagnostics.Debug.WriteLine("count: " + count.ToString());

            networks = new List<byte[]>();



            int networkInfo = 60 + count + 31;
            int networkCount = data[networkInfo];
            System.Diagnostics.Debug.WriteLine("NetworkCount: " + networkCount.ToString());
            int networkPosition = 61;
            for (int i = 2; i <= networkCount * 2; i += 2)
            {
                int elementCount = data[networkInfo + i];
                System.Diagnostics.Debug.WriteLine("ElementCount: " + elementCount.ToString());
                if (elementCount != 0)
                {
                    byte[] tmp = new byte[elementCount];
                    for (int j = 0; j < elementCount; j++)
                    {
                        tmp[j] = data[networkPosition + j];
                    }
                    networks.Add(tmp);
                }
                networkPosition += elementCount;

                //data[networkInfo + i]
            }

            for (int i = 0; i < networks.Count; i++)
            {
                System.Diagnostics.Debug.Write("Elements: ");
                for (int j = 0; j < networks[i].Length; j++)
                {
                    System.Diagnostics.Debug.Write(networks[i][j] + ",");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            ////+31 info o ilości networków i ilości elementów w danym networku
            return networks;
        }

    }
}
