using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;

namespace Block_sorting_system
{
    class ControlMode
    {
        public enum ModeList
        {
            Manual,
            Auto
        }
        
        public static Sensor auto = new Sensor(new Vector2(863, 225), new RectangleF(0, 0, 22, 22), "auto");
        public static Sensor manual = new Sensor(new Vector2(815, 225), new RectangleF(0, 0, 22, 22), "manual");
        private static ModeList _mode = ModeList.Manual;
        public static ModeList Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (value == ModeList.Auto)
                {
                    auto.State = true;
                    manual.State = false;
                }
                else if (value == ModeList.Manual)
                {
                    auto.State = false;
                    manual.State = true;
                }
                _mode = value;
            }
        }

        public static void Init()
        {
            _mode = ModeList.Manual;
            auto.State = false;
            manual.State = true;
        }

        public static void UpdateControlsForMode()
        {
            auto.Draw();
            manual.Draw();
        }
    }
}
