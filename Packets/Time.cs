using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class Time
    {
        public byte Year {get;set;}
        public byte Month {get;set;}
        public byte Day {get;set;}
        public byte Hour {get;set;}
        public byte Minute {get;set;}
        public byte Second {get;set;}
        public byte Milisec { get; set; }
        public int Milisecond { get; set; }

        public Time()
        {
            DateTime dt = DateTime.Now;
            Year = Convert.ToByte(Convert.ToInt16(dt.Year.ToString().Substring(2), 16));
            Month = Convert.ToByte(Convert.ToInt16(dt.Month.ToString(), 16));
            Day = Convert.ToByte(Convert.ToInt16(dt.Day.ToString(), 16));
            Hour = Convert.ToByte(Convert.ToInt16(dt.Hour.ToString(), 16));
            Minute = Convert.ToByte(Convert.ToInt16(dt.Minute.ToString(), 16));
            Second = Convert.ToByte(Convert.ToInt16(dt.Second.ToString(), 16));
            Milisecond = Convert.ToInt16(dt.Millisecond.ToString(), 16);
            switch (dt.Millisecond.ToString().Length)
            {
                case 1:
                    Milisec = Convert.ToByte(dt.Millisecond);
                    break;
                case 2:
                    Milisec = Convert.ToByte(dt.Millisecond.ToString().Substring(0, 1));
                    break;
                case 3:
                    Milisec = Convert.ToByte(dt.Millisecond.ToString().Substring(0, 2));
                    break;
            }
            
        }
    }
}
