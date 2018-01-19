using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_sorting_system
{
    public class Gripper:IState
    {
        public enum StateList
        {
            Open,
            Close
        }

        public bool State
        {
            get
            {
                bool stateTmp = false;
                if (state == Gripper.StateList.Close)
                {
                    stateTmp = false;
                }
                else if (state == Gripper.StateList.Open)
                {
                    stateTmp = true;
                }
                return stateTmp;
            }
        }
        public StateList state;
        public static bool isPossibleToChangeState = true;

        public static bool butonik = false;

        public Gripper()
        {
            state = StateList.Open;
        }
    }
}
