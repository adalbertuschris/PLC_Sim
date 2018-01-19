using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.IO;
using System.Xml;

namespace Symulator_PLC
{
    class VirtualSystem
    {
        private static System.Diagnostics.Process _runningProcess = new System.Diagnostics.Process();
        public static System.Diagnostics.Process RunningProcess
        {
            get
            {
                return _runningProcess;
            }
            set
            {
                _runningProcess = value;
            }
        }

        public static bool onClosed = false;
        public static string Name { get; set; }
        public static int NumberOfDigitalSensors { get; set; }
        public static int NumberOfDigitalActuators { get; set; }
        public static string Description { get; set; }
        public static string AssemblySources { get; set; }
        public static Picture picture = new Picture();
        public static Picture connectionInfoPicture = new Picture();

        public static List<string> VirtualSystemList(string filePath)
        {
            List<string> filesList = null;
            try
            {
                filesList = BrowserDirectories.InspectDirectories(filePath, "xml", false);                
            }
            catch (DirectoryNotFoundException)
            {
                throw new DirectoryNotFoundException("Check if directory '" + filePath + "' is not deleted");
            }
            catch (FileNotFoundException fnfe)
            {
                throw new FileNotFoundException(fnfe.Message);
            }
            return filesList;
            
        }

        public static void Load(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream("Virtual System\\" + filePath, FileMode.Open, FileAccess.Read))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);

                    XmlNode xmlInfo = doc.DocumentElement.SelectSingleNode("info");
                    Name = xmlInfo.SelectSingleNode("name").FirstChild.Value;
                    NumberOfDigitalSensors = Convert.ToInt32(xmlInfo.SelectSingleNode("numberOfDigitalSensors").FirstChild.Value);
                    NumberOfDigitalActuators = Convert.ToInt32(xmlInfo.SelectSingleNode("numberOfDigitalActuators").FirstChild.Value);
                    Description = xmlInfo.SelectSingleNode("description").FirstChild.Value;
                   
                    XmlNode xmlImage = xmlInfo.SelectSingleNode("image");
                    picture.Width = Convert.ToInt32(xmlImage.Attributes["width"].Value);
                    picture.Height = Convert.ToInt32(xmlImage.Attributes["height"].Value);
                    picture.FilePath = @"Virtual System\" + xmlImage.Attributes["source"].Value;

                    XmlNode xmlConnectionInfoImage = xmlInfo.SelectSingleNode("connectionInfoImage");
                    connectionInfoPicture.Width = Convert.ToInt32(xmlConnectionInfoImage.Attributes["width"].Value);
                    connectionInfoPicture.Height = Convert.ToInt32(xmlConnectionInfoImage.Attributes["height"].Value);
                    connectionInfoPicture.FilePath = @"Virtual System\" + xmlConnectionInfoImage.Attributes["source"].Value;


                    XmlNode xmlAssembly = doc.DocumentElement.SelectSingleNode("assembly");
                    AssemblySources = @"Virtual System\" + xmlAssembly.Attributes["source"].Value;                

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
                throw new Devices.FileStructIsNotCorectException(filePath);
            }
        }

    }
    class Picture
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string FilePath { get; set; }
    }


}
