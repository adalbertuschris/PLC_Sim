using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.Xml;
using System.IO;

namespace Devices
{
    public class Device
    {
        private static Device[] devices;
        public List<Module> modules = new List<Module>();
        public string FamilyName { get; set; }

        //public Device() { }
        //{
            
        //    InitTabDevices();
        //}

        private static void InitTabDevices(int countDevices)
        {
            devices = new Device[countDevices];
            for (int i = 0; i < devices.Length; i++)
            {
                devices[i] = new Device();
            }
        }

        public static Device[] ListDevices(string filePath)
        {
            try
            {
                var filesList = BrowserDirectories.InspectDirectories(filePath, "xml", true);

                int countDevices = filesList.Count;
                InitTabDevices(countDevices);
                int counter = 0;

                foreach (var file in filesList)
                {
                    using (FileStream stream = new FileStream(filePath + file, FileMode.Open, FileAccess.Read))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(stream);

                        XmlNode device = doc.DocumentElement.SelectSingleNode("device");
                        devices[counter].FamilyName = device.SelectSingleNode("family").Attributes["name"].Value;

                        XmlNodeList modules = device.SelectSingleNode("family").SelectNodes("module");

                        for (int i = 0; i < modules.Count; i++)
                        {
                            Module module = new Module();
                            module.Name = modules[i].Attributes["name"].Value;
                            module.ShortDesignation = modules[i].SelectSingleNode("shortDesignation").FirstChild.Value;
                            module.OrderNumber = modules[i].SelectSingleNode("orderNumber").FirstChild.Value;
                            module.Version = modules[i].SelectSingleNode("firmwareVersion").FirstChild.Value;

                            devices[counter].modules.Add(module);
                        }
                    }
                    counter++;
                }
            }
            catch(DirectoryNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show("Check if directory '" + filePath + "' is not deleted");
            }
            catch (FileNotFoundException fnfe)
            {
                devices = null;
                System.Windows.Forms.MessageBox.Show(fnfe.Message + 
                    "\n\rMożliwe przyczyny:"+
                    "\n\r1. Brak rozszerzenia w wyszukiwanej nazwie pliku"+
                    "\n\r2. Plik został usunięty w trakcie uruchamiania bądź działania programu.");
            }

            return devices;
        }
    }
}
