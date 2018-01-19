using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace Services
{
    public class ServiceManagement
    {
        public enum service { run, stop };
        public static void ServiceOperation(string name, service action)
        {
            ServiceController myService = new ServiceController(name);

            try
            {
                switch (action)
                {
                    case service.run:
                        if (myService.Status == ServiceControllerStatus.Stopped)
                        {
                            myService.Start();
                            myService.WaitForStatus(ServiceControllerStatus.Running);
                        }
                        break;

                    case service.stop:
                        if (myService.Status == ServiceControllerStatus.Running)
                        {
                            myService.Stop();
                            myService.WaitForStatus(ServiceControllerStatus.Stopped);
                        }
                        break;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
