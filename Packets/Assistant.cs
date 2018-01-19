using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public class Assistant:IAssistant
    {
        bool _isSend;
        bool _isReceive;
        public bool SomethingToSend
        {
            get { return _isSend; }
            set { _isSend = value; }
        }
        public bool SomethingToReceive
        {
            get { return _isReceive; }
            set { _isReceive = value; }
        }
        public byte[] DataToSend
        {
            get;
            set;
        }
      
    }
}
