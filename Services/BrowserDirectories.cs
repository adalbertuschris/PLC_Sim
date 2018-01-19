using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Services
{
    public class BrowserDirectories
    {
        public static List<string> InspectDirectories(string directoryToSearch)
        {
            var allFilePaths = from file in Directory.GetFiles(directoryToSearch)
                               select file;

            var fileName = from filePath in allFilePaths
                           let fileNameWithoutPath = Path.GetFileName(filePath)
                           select fileNameWithoutPath;

            return fileName.ToList();

        }

        public static List<string> InspectDirectories(string directoryToSearch, string fileExtension, bool returnNameWithExtension)
        {
            //var searchOption = SearchOption.TopDirectoryOnly;
            var allFilePaths = from file in Directory.GetFiles(directoryToSearch)
                               select file;

            IEnumerable<string> fileName;

            if (returnNameWithExtension)
            {
                fileName = from filePath in allFilePaths
                               where Path.GetExtension(filePath) == ("." + fileExtension)
                               let fileNameWithoutPath = Path.GetFileName(filePath)
                               select fileNameWithoutPath;
            }
            else
            {
                fileName = from filePath in allFilePaths
                               where Path.GetExtension(filePath) == ("." + fileExtension)
                               let fileNameWithoutPath = Path.GetFileNameWithoutExtension(filePath)
                               select fileNameWithoutPath;
            }

            return fileName.ToList();
        }        
    }
}
