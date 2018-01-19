using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public class CustomEventArgs : EventArgs
    {
        public string str = "";
        public CustomEventArgs(byte[] data)
        {
            CreateString(data);
        }
        void CreateString(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                str += data[i] + ",";
            }

        }
    }
}
