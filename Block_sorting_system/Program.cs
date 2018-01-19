using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using OpenTK;
using System.IO;
using OpenTK.Input;


namespace Block_sorting_system
{
    class Program
    {
        static void Main(string[] args)
        {        
            ThreadPool.QueueUserWorkItem(delegate
            {
                DataTransfer.PipeClient.StartConnecting();
            }
            );
            Game window = new Game(900, 480, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4));
            window.Title = "Block sorting system";
            if (File.Exists("Content/BSSicon.ico"))
            {
                window.Icon = new Icon("Content/BSSicon.ico");
            }
            else if (File.Exists("Virtual System/Block sorting system/Content/BSSicon.ico"))
            {
                window.Icon = new Icon("Virtual System/Block sorting system/Content/BSSicon.ico");
            }
            else
            {
            }
            window.WindowBorder = WindowBorder.Fixed;
            window.Run();
            DataTransfer.PipeClient.CloseConnecting();
        }
    }
}
