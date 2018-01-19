using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Sockets;

namespace Packets
{
    public class S7Packet
    {
        public static bool CheckCode(byte[] dataReceived, IAssistant assistant)
        {
            if (dataReceived[5] == 224)
            {
                if (dataReceived[18] != 4)
                {
                    assistant.DataToSend = COTPConnectionPacket.ConnectionConfirmLoad(dataReceived).ToArray();
                    return true;
                }
                else
                {
                    assistant.DataToSend = new byte[] { 3, 0, 0, 33, 2, 240, 128, 50, 7, 0, 0, 193, 2, 0, 12, 0, 4, 0, 1, 18, 8, 18, 131, 0, 0, 0, 0, 210, 9, 10, 0, 0, 0 };
                    return true;
                }
            }
            else if (dataReceived[5] == 240)
            {

                if (S7PacketGenerator.Check(dataReceived))
                {
                    assistant.DataToSend = S7PacketGenerator.GetDataToSend().ToArray();                    
                    return true;
                }
            }            
            return false;
        }
    }
}
