using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;

namespace Block_sorting_system
{
    public class InductiveSensor : Sensor
    {

        public InductiveSensor(Vector2 pos, RectangleF size, Vector2 positionObjectDetected, string name)
            : base(pos, size, positionObjectDetected, name)
        {
        }
        public override bool CheckPosition(IPosition ob)
        {
            bool tmp = false;
            if (positionObjectDetected == ob.Position)
            {
                Block block = ob as Block;
                if (block.type == Block.Type.Silver)
                {
                    tmp = true;
                }

            }
            return tmp;
        }
    }
}
