using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Services
{
    public class ProcessInfo
    {
        public static bool CheckWhetherRunning(string processName)
        {
            var RunningProcesses = Process.GetProcesses(".");

            foreach (var process in RunningProcesses)
            {
                Debug.WriteLine(process.ProcessName);
                if (process.ProcessName.Equals(processName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
