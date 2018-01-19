using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_sorting_system
{
    public struct Texture2D
    {
        private int id;
        private int width, height;

        public int ID
        {
            get { return id; }
        }

        public int Width
        { get { return width; } }

        public int Height {
            get { return height; }
        }

        public Texture2D(int id, int width, int height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }
    }
}
