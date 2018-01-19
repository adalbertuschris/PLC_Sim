using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public interface IAssistant
    {
        bool SomethingToSend
        {
            get;
            set;
        }
        bool SomethingToReceive
        {
            get;
            set;
        }   
        byte[] DataToSend
        {
            get;
            set;
        }
    }
}
