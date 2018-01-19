using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Block_sorting_system
{
    public class Conveyor: IState//Podmienione
    {
        public enum StateList
        {
            Run,
            Stop
        }

        public bool State
        {
            get
            {
                bool stateTmp = false;
                if (state == Conveyor.StateList.Stop)
                {
                    stateTmp = false;
                }
                else if (state == Conveyor.StateList.Run)
                {
                    stateTmp = true;
                }
                return stateTmp;
            }
        }
        
        public StateList state;

        public static bool butonik = false;

        public Conveyor()
        {            
            state = StateList.Stop;
        }

        //public void Update()
        //{            
        //}

        //public void Draw()
        //{            
        //}
    }
}
