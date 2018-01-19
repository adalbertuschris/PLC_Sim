using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    class COTPConnectionPacket
    {
        static byte tpdu_code = 0;
        static byte tpdu_size = 0;
        //static byte li = 0;
        static byte class_option = 0;
        static byte[] dst_ref, src_ref;
        static byte[] variable_part;
        static List<byte> stream = new List<byte>();
        //COTP_ConnectionRequest
        public static List<byte> ConnectionConfirmLoad(byte [] data)
        {
            stream = new List<byte>();
            tpdu_code = 208;
            tpdu_size = data[data.Length - 1];
            if (data[18] == 2)
            {
                dst_ref = new byte[2] { 0, 1 };
                src_ref = new byte[2] { 0, 2 };
            }
            else if (data[18] == 0)
            {
                dst_ref = new byte[2] { data[6], data[7] };
                src_ref = new byte[2] { data[8], data[9] };
            }
            
            //variable part
            //Calling T
            int len_var_part = data[4]-6;
            variable_part = new byte[len_var_part];
            for(int i =0; i < len_var_part; i++)
            {
                variable_part[i] = data[11+i];            
            }
            int packet_length = 11 + len_var_part;
            //byte[] pack = new byte[11 + len_var_part];
            Pack(TPKTPacket.Load(packet_length));
            Pack(new byte[1] { Convert.ToByte(packet_length - 5) });
            Pack(new byte[1] { tpdu_code });
            Pack(dst_ref);
            Pack(src_ref);
            Pack(new byte[1] { class_option });
            Pack(variable_part);
            return stream;
        }
        //3, 0, 0, 22, 17, 224, 0, 0, 0, 178, 0, 193, 2, 1, 0, 194, 2, 1, 2, 192, 1, 10
        //COTP_ConnectionConfirm
        //3,0,0,22,17,208,0,1,0,2,0,193,2,1,0,194,2,1,2,192,1,10
        public static void Pack(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                stream.Add(data[i]);
            }
        }

    }
}
