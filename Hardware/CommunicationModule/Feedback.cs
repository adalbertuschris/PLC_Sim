using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CommunicationModule
{
    public class Feedback
    {
        private bool _isSet;

        public bool IsSet
        {
            get
            {
                return _isSet;
            }
        }


        public SendInfo sendInfo;
        public void SetFeedback(SendInfo sendInfo)
        {
            this.sendInfo = sendInfo;
            _isSet = true;
            //System.Diagnostics.Debug.WriteLine("SET FEEDBACK");
        }
    }
}
