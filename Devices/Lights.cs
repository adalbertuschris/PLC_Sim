using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Devices
{
    public class Lights
    {
        public bool Checked { get; set; }
        public bool ReadOnly { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Position { get; set; }
    }
}
