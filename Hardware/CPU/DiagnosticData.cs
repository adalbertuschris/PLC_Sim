using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CPU
{
    public delegate void DiagDataDelegate(List<byte> message);
    public class DiagnosticData
    {
        public static event DiagDataDelegate Diagnostic;
        public enum Operation
        {
            Set,
            Reset,
            None
        }
        public enum Symbol
        {
            standard,
            special
        }
        public enum Modifier
        {
            not,
            none
        }
        private static byte counterDiagData = 7;
        public static bool diagDataSpecial;
        public static List<byte> diagData = new List<byte>(new byte[] { 0x00, 0x00, 0x00, 0x01});
        public static void DiagnosticOperation()
        {
            List<byte> message = new List<byte>(diagData);
            if (Diagnostic != null)
            {
                Diagnostic(message);
            }
            if (diagDataSpecial)
            {
                counterDiagData = 3;
            }
            else
            {
                counterDiagData = 7;
            }
        }
        
        public static void AddData(int counter, Operation operation, Symbol symbol)
        {
            diagData.Add(0x00);
            diagData.Add(Convert.ToByte(counter));
            diagData.Add(0x00);
            if (operation == Operation.None && symbol == Symbol.standard)
            {
                diagData.Add(0x01);
            }
            else if ((operation == Operation.None && symbol == Symbol.special) || 
                ((operation == Operation.Set || operation == Operation.Reset) && symbol == Symbol.standard))
            {
                diagData.Add(0x00);
            }
        }

        public static void AddDataSpecial()
        {
            diagData = new List<byte>(new byte[] { 0x00, 0x02, 0x00, 0x01 });
            counterDiagData = 3;
            diagDataSpecial = true;
        }

        public static void ResetData()
        {
            diagData = new List<byte>(new byte[] { 0x00, 0x00, 0x00, 0x01 });
            counterDiagData = 7;
            diagDataSpecial = false;
        }

        public static void RefreshState(Operation operation, bool state, Symbol symbol = Symbol.standard, Modifier modifier = Modifier.none)
        {
            if (operation == Operation.None)
            {
                if (symbol == Symbol.standard && modifier == Modifier.none)
                {
                    if (state)
                    {
                        diagData[counterDiagData] = 0x07;
                    }
                    else
                    {
                        diagData[counterDiagData] = 0x01;
                    }
                }
                else if (symbol == Symbol.standard && modifier == Modifier.not)
                {
                    if (state)
                    {
                        diagData[counterDiagData] = 0x03;
                    }
                    else
                    {
                        diagData[counterDiagData] = 0x05;
                    }
                }
                else if (symbol == Symbol.special)
                {
                    if (state)
                    {
                        diagData[counterDiagData] = 0x06;
                    }
                    else
                    {
                        diagData[counterDiagData] = 0x00;
                    }
                }
            }
            else if (operation == Operation.Set)
            {
                if (state)
                {
                    diagData[counterDiagData] = 0x06;
                }
                else
                {
                    diagData[counterDiagData] = 0x00;
                }
            }
            else if (operation == Operation.Reset)
            {
                if (state)
                {
                    diagData[counterDiagData] = 0x02;
                }
                else
                {
                    diagData[counterDiagData] = 0x00;
                }
            }            
            counterDiagData +=4;
        }
    }
}
