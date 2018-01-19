using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public class ReceiveInfo
    {
        private readonly int _count;
        private readonly byte[] _data;

        public byte[] Data
        {
            get { return _data; }
        }

        public int Count
        {
            get { return _count; }
        }

        public ReceiveInfo(byte[] data, int bytesReads)
        {
            _data = data;
            _count = bytesReads;
        }
    }
}
