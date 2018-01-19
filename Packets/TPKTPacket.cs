using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class TPKTPacket
    {
        static byte[] user_data;
        static int version = 0;
        static int reserved = 0;
        static int length = 0;
        public static byte[] Load(int packet_length)
        {
            version = 3;
            length = packet_length;
            user_data = new byte[4];
            user_data[0] = Convert.ToByte(version);
            user_data[1] = Convert.ToByte(reserved);
            user_data[2] = 0;
            user_data[3] = Convert.ToByte(length);
            return user_data;
        }

    }
}
