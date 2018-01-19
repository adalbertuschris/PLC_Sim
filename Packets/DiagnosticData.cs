using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    class DiagnosticData
    {
        public static byte startAddressAWL;
        public static byte stepAddressCounter;
        public static List<byte> diagnosticData;
        public static List<byte> GetDiagDataToSend(List<byte> data)
        {
            var dataToSend = new List<byte>();
            for (int i = 1; i < data.Count; i+=4)
            {
                if (dataToSend.Count == (stepAddressCounter + 2) * 2)
                {
                    break;
                }
                if (data[i] >= startAddressAWL)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dataToSend.Add(data[i - 1 + j]);
                    }
                }
            }
            return dataToSend;
        }
    }
}
