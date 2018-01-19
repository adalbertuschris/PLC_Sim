using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;

namespace Block_sorting_system
{
    public class Sensor
    {

        private Texture2D sensorOnSprite;
        private Texture2D sensorOffSprite;
        private Texture2D actualStateSensor;
        private Vector2 position;
        private RectangleF size;
        public string Name { get; set; }
        public Vector2 positionObjectDetected;
        public bool State { get; set; }

        public static bool butonik = false;

        public Sensor(Vector2 pos, RectangleF size, Vector2 positionObjectDetected, string name)
        {
            position = pos;
            this.size = size;
            this.positionObjectDetected = positionObjectDetected;
            sensorOnSprite = ContentPipe.LoadTexture("sensorOn.png");
            sensorOffSprite = ContentPipe.LoadTexture("sensorOff.png");
            actualStateSensor = sensorOffSprite;
            Name = name;
        }

        public Sensor(Vector2 pos, RectangleF size, string name)
        {
            position = pos;
            this.size = size;            
            sensorOnSprite = ContentPipe.LoadTexture("sensorOn.png");
            sensorOffSprite = ContentPipe.LoadTexture("sensorOff.png");
            actualStateSensor = sensorOffSprite;
            Name = name;
        }


        public virtual bool CheckPosition(IPosition ob)
        {
            if (positionObjectDetected == ob.Position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckState(IState ob)
        {
            if (ob.State)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw()
        {
            if (State)
            {
                Spritebatch.Draw(sensorOnSprite, position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), size);
            }
            else
            {
                Spritebatch.Draw(sensorOffSprite, position, new Vector2(1f, 1f), Color.White, new Vector2(0, 0), size);
            }
        }
    }
}
