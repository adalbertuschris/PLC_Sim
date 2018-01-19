using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices;
using System.Threading;

namespace Hardware
{
    public delegate void GetSetIOStateDelegate();
    public class PLC
    {
        private static bool stop = true;
        public static event GetSetIOStateDelegate GetSetIOState;
        public static Dictionary<string, LED> InputModule;
        public static Dictionary<string, LED> OutputModule;
        public static Dictionary<string, LED> CpuModule;
        public static PLCInfo Info { get; set; }
        private ComponentsForModule _IOModule = new ComponentsForModule();

        public static void Run()
        {
            while (true)
            {
                if (!stop)
                {
                    CPU.Operation.ProgramSweep(CPU.Memory.Program.InstructionList, ComponentsForModule.InputControls, ComponentsForModule.OutputControls);                             
                }
                ComponentsForModule.RefreshLed();
                if (GetSetIOState != null)
                {
                    GetSetIOState();
                }           
                Thread.Sleep(1);
            }
        }

        public PLC(string filePath)
        {
            Info = new PLCInfo(filePath);
            _IOModule.Initialize(Info);
            InputModule = ComponentsForModule.InputControls;
            OutputModule = ComponentsForModule.OutputControls;
            CpuModule = ComponentsForModule.StatusControls;
            CPU.Memory.Data.Init(InputModule, OutputModule);
            
        }
        public static void WriteProgramToMemory(List<byte[]> networkList)
        {
            //using (System.IO.StreamWriter file =
            //new System.IO.StreamWriter(@"C:\Users\Wojciech\Documents\ProgramSterowania.txt"))
            //{
                stop = true;
                CPU.Operation.ResetMemory();
                CPU.DiagnosticData.ResetData();
                CPU.CommandRegister.Operations.CreateInstructionList(networkList);
                for (int i = 0; i < CPU.Memory.Program.InstructionList.Count; i++)
                {
                    System.Diagnostics.Debug.Write(CPU.Memory.Program.InstructionList[i].operatorIL);
                    
                    System.Diagnostics.Debug.Write(" :   ");
                    if (CPU.Memory.Program.InstructionList[i].operand != null)
                    {
                        //file.WriteLine(CPU.Memory.Program.InstructionList[i].operatorIL + " :   " + CPU.Memory.Program.InstructionList[i].operand.name);
                        System.Diagnostics.Debug.WriteLine(CPU.Memory.Program.InstructionList[i].operand.name);
                    }
                    else
                    {
                        //file.WriteLine(CPU.Memory.Program.InstructionList[i].operatorIL);
                        System.Diagnostics.Debug.WriteLine("");
                    }
                }
                stop = false;
            //}

        }
    }
}
