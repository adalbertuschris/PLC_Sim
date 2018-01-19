using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public class DownloadInfo
    {
        public static List<byte> dataDownloaded;
        public static bool downloadEnded = false;
        public static bool firstPartDataSaved = false;
        public static List<byte> OBblockInfoPart = new List<byte>();
        public static int parameterData = 0;
        public static int protocolDataUnitReference = 0;
        public static byte blockLanguage;
        public static byte subblkType;
        public static byte[] blockNumber;
        public static byte[] lengthLoadMemory;
        public static byte[] blockSecurity;
        public static byte[] codeTimestamp;
        public static byte[] interfaceTimestamp;
        public static byte[] ssbLength;
        public static byte[] addLength;
        public static byte[] localDataLength;
        public static byte[] mc7Code;

        public static void SaveData(byte[] data)
        {            
            if (!firstPartDataSaved)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    dataDownloaded.Add(data[i]);
                }
                OBblockInfoPart = new List<byte>();
                blockLanguage = data[29];
                subblkType = data[30];
                blockNumber = new byte[] { data[31], data[32] };
                lengthLoadMemory = new byte[] { data[33], data[34], data[35], data[36] };
                blockSecurity = new byte[] { data[37], data[38], data[39], data[40] };
                codeTimestamp = new byte[] { data[41], data[42], data[43], data[44], data[45], data[46] };
                interfaceTimestamp = new byte[] { data[47], data[48], data[49], data[50], data[51], data[52] };
                ssbLength = new byte[] { data[53], data[54] };
                addLength = new byte[] { data[55], data[56] };
                localDataLength = new byte[] { data[57], data[58] };
                mc7Code = new byte[] { data[59], data[60] };
            }
            else
            {
                for (int i = 25; i < data.Length; i++)
                {
                    dataDownloaded.Add(data[i]);
                }
            }
            firstPartDataSaved = true;
        }

        public static byte[] GetDownloadInfo()
        {
            OBblockInfoPart = new List<byte>();
            Pack(blockLanguage, OBblockInfoPart);
            Pack(subblkType, OBblockInfoPart);
            Pack(blockNumber, OBblockInfoPart);
            Pack(lengthLoadMemory, OBblockInfoPart);
            Pack(blockSecurity, OBblockInfoPart);
            Pack(codeTimestamp, OBblockInfoPart);
            Pack(interfaceTimestamp, OBblockInfoPart);
            Pack(ssbLength, OBblockInfoPart);
            Pack(addLength, OBblockInfoPart);
            Pack(localDataLength, OBblockInfoPart);
            Pack(mc7Code, OBblockInfoPart);
            return OBblockInfoPart.ToArray();
        }

        public static void Pack(byte[] data, List<byte> streamToCreate)
        {
            for (int i = 0; i < data.Length; i++)
            {
                streamToCreate.Add(data[i]);
            }
        }
        public static void Pack(byte data, List<byte> streamToCreate)
        {
            streamToCreate.Add(data);
        }
    }
}
