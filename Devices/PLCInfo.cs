using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.IO;
using System.Xml;

namespace Devices
{
    public class PLCInfo
    {
        public Device device = new Device();
        public Picture picture = new Picture();
        public Dictionary<string, Lights> lights = new Dictionary<string, Lights>();

        public PLCInfo(string filePath)
        {
            try
            {
                filePath = filePath.Replace('/', '_');
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);

                    XmlNode xmlDevice = doc.DocumentElement.SelectSingleNode("device");
                    device.FamilyName = xmlDevice.SelectSingleNode("family").Attributes["name"].Value;

                    XmlNodeList xmlModules = xmlDevice.SelectSingleNode("family").SelectNodes("module");

                    for (int i = 0; i < xmlModules.Count; i++)
                    {
                        Module module = new Module();
                        module.Name = xmlModules[i].Attributes["name"].Value;
                        module.ShortDesignation = xmlModules[i].SelectSingleNode("shortDesignation").FirstChild.Value;
                        module.OrderNumber = xmlModules[i].SelectSingleNode("orderNumber").FirstChild.Value;
                        module.Version = xmlModules[i].SelectSingleNode("firmwareVersion").FirstChild.Value;

                        device.modules.Add(module);
                    }

                    XmlNode xmlView = doc.DocumentElement.SelectSingleNode("view");
                    XmlNode xmlImage = xmlView.SelectSingleNode("window").SelectSingleNode("image");
                    picture.Width = Convert.ToInt32(xmlImage.Attributes["width"].Value);
                    picture.Height = Convert.ToInt32(xmlImage.Attributes["height"].Value);
                    picture.FilePath = @"Devices\" + xmlImage.Attributes["source"].Value;

                    XmlNodeList xmlControls = xmlView.SelectSingleNode("window").SelectNodes("controls");

                    for (int i = 0; i < xmlControls.Count; i++)
                    {
                        Lights light = new Lights();
                        string nameKey = xmlControls[i].Attributes["name"].Value;

                        light.Checked = Convert.ToBoolean(xmlControls[i].Attributes["checked"].Value);
                        light.ReadOnly = Convert.ToBoolean(xmlControls[i].Attributes["readOnly"].Value);
                        light.Height = Convert.ToInt32(xmlControls[i].Attributes["height"].Value);
                        light.Width = Convert.ToInt32(xmlControls[i].Attributes["width"].Value);
                        int posX = Convert.ToInt32(xmlControls[i].Attributes["locationX"].Value);
                        int posY = Convert.ToInt32(xmlControls[i].Attributes["locationY"].Value);
                        light.Position = new System.Drawing.Point(posX, posY);

                        lights.Add(nameKey, light);
                    }
                }
            }

            catch (FileNotFoundException fnfe)
            {
                throw new FileNotFoundException(fnfe.Message + 
                    "\n\rMożliwe przyczyny:" +
                    "\n\r1. Plik został usunięty w trakcie uruchamiania bądź działania programu.");
            }
            catch (NullReferenceException)
            {
                throw new FileStructIsNotCorectException(filePath);
            }
        }
    }
}

