using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace Services
{
    public class NetInfo
    {
        private static Dictionary<string, string> networkAdapter;

        private static void GetAvailableNetworkInterfaces()
        {
            networkAdapter = new Dictionary<string, string>();
            IPGlobalProperties compProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics != null && nics.Length > 0)
            {
                foreach (NetworkInterface adapter in nics)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    if (adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback && adapter.NetworkInterfaceType != NetworkInterfaceType.Unknown)
                    {
                        if (adapter.OperationalStatus == OperationalStatus.Up && adapter.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            string ip = properties.UnicastAddresses[1].Address.ToString();
                            string name = adapter.Description;
                            networkAdapter.Add(name, ip);
                            //adapterName.Add(adapter.Description);
                        }
                    }
                }
            }            
        }

        public static List<string> GetAvailableAdapterName()
        {
            GetAvailableNetworkInterfaces();
            try
            {
                 return networkAdapter.Keys.ToList<string>();     
            }
            catch
            {
                throw new Exception("Check if your network connection is active");
            }
                  
        }

        public static string GetIP(string name)
        {            
            try
            {
                return networkAdapter[name];
            }
            catch
            {
                throw new Exception("Check if your network adapter is enabled");
            }
        }
    }
}

