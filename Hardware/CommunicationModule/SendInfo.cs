using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public class SendInfo
    {
        private readonly byte[] _data;
        private readonly bool _somethingToSend;
        private readonly bool _somethingToReceive;

        public byte[] DataToSend
        {
            get
            {
                return _data;
            }
        }

        public bool SomethingToSend
        {
            get
            {
                return _somethingToSend;
            }
        }

        public bool SomethingToReceive
        {
            get
            {
                return _somethingToReceive;
            }
        }

        public SendInfo(bool somethingToSend, bool somethingToReceive)
        {
            _somethingToSend = somethingToSend;
            _somethingToReceive = somethingToReceive;
            //System.Diagnostics.Debug.WriteLine("SEND INFO 1");
        }

        public SendInfo(bool somethingToSend, bool somethingToReceive, byte[] data)
        {
            _somethingToSend = somethingToSend;
            _somethingToReceive = somethingToReceive;
            _data = data;
            //System.Diagnostics.Debug.WriteLine("SEND INFO 2");
        }
    }
}
