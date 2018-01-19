using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Hardware.CommunicationModule;
using System.Diagnostics;
using System.Security.Principal;

namespace Symulator_PLC
{
    static class Program
    {
        static Boolean IfAdministrator()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void LiftPrivileges()
        {
            // Uruchamia samego siebie jako administrator
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;

            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";

            try
            {
                Process.Start(proc);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Services.ProcessInfo.CheckWhetherRunning("Siemens.Automation.Portal"))
            {
                MessageBox.Show("Przed uruchomieniem symulatora, zamknij środowisko TIA Portal");
                return;
            }
            else
            {
                if (!IfAdministrator())
                {
                    LiftPrivileges();                    
                    return;
                }

                Thread tr = new Thread(CommunicationModule.Run);

                if (Services.ProcessInfo.CheckWhetherRunning("s7oiehsx64"))
                {
                    Services.ServiceManagement.ServiceOperation("s7oiehsx64", Services.ServiceManagement.service.stop);
                    try
                    {
                        tr.Start();
                    }
                    catch { }
                    Services.ServiceManagement.ServiceOperation("s7oiehsx64", Services.ServiceManagement.service.run);
                }
                else
                {
                    try
                    {
                        tr.Start();
                    }
                    catch { }
                }
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                try
                {
                    CommunicationModule.Stop();
                    bool isRun = false;
                    if (VirtualSystem.RunningProcess.StartInfo.FileName != "")
                    {
                        if (!VirtualSystem.RunningProcess.HasExited)
                        {
                            isRun = true;
                        }
                    }
                    else
                    {
                        isRun = false;
                    }
                    if (isRun)
                    {
                        VirtualSystem.RunningProcess.Kill();
                    }
                }
                catch { }
            }
        }
    }
}
