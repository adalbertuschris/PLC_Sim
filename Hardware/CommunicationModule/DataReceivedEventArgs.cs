using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public class DataReceivedEventArgs : EventArgs
    {
        private readonly ReceiveInfo _receiveInfo;
        private Feedback _feedback;

        public ReceiveInfo RcvInfo
        {
            get { return _receiveInfo; }
        }

        public Feedback FeedBack
        {
            get { return _feedback; }
        }

        public DataReceivedEventArgs(ReceiveInfo recInfo, Feedback feedback)
        {
            _receiveInfo = recInfo;
            _feedback = feedback;
        }
    }
}
