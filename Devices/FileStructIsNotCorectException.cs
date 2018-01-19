using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    public class FileStructIsNotCorectException:Exception
    {
        private readonly string _message;
        public new string Message
        {
            get
            {
                return _message;
            }
        }
        public FileStructIsNotCorectException(string nameFile)
        {
            _message = "Struktura pliku xml jest niepoprawna\n\rNazwa wadliwego pliku: "+nameFile;
        }
    }
}
