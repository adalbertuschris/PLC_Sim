using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Devices;

namespace Hardware
{
    internal class ComponentsForModule
    {
        public static Dictionary<string, LED> InputControls = new Dictionary<string, LED>();
        public static Dictionary<string, LED> OutputControls = new Dictionary<string, LED>();
        public static Dictionary<string, LED> StatusControls = new Dictionary<string, LED>();

        public void Initialize(PLCInfo plc)
        {
            foreach (var light in plc.lights)
            {
                LED led = new LED();
                led.Name = light.Key;
                led.ReadOnly = light.Value.ReadOnly;
                led.State = light.Value.Checked;
                if (light.Value.Checked)
                {
                    led.BackColor = Color.Gold;
                }
                else
                {
                    led.BackColor = Color.Transparent;
                }
                led.Size = new System.Drawing.Size(light.Value.Width, light.Value.Height);
                led.Location = light.Value.Position;

                if (led.Name[0] == 'I')
                {
                    led.Click += LightsClick;
                    InputControls.Add(light.Key, led);
                }
                else if (led.Name[0] == 'Q')
                {
                    OutputControls.Add(light.Key, led);
                }
                else
                {
                    StatusControls.Add(light.Key, led);
                }
            }
        }

        private void LightsClick(object sender, EventArgs e)
        {
            LED tmp = sender as LED;
            LED led = InputControls[tmp.Name];

            if (!led.ReadOnly)
            {
                if (led.State)
                {
                    led.BackColor = Color.Transparent;
                    led.State = false;
                }
                else
                {
                    led.BackColor = Color.Gold;
                    led.State = true;
                }
            }
            //MessageBox.Show(led.Name);
            //}
        }

        public static void RefreshLed()
        {
            List<string> inputKeyList = new List<string>(ComponentsForModule.InputControls.Keys);
            List<string> outputKeyList = new List<string>(ComponentsForModule.OutputControls.Keys);

            for (int i = 0; i < inputKeyList.Count; i++)
            {
                LED led = ComponentsForModule.InputControls[inputKeyList[i]];

                if (led.State)
                {
                    led.BackColor = Color.Gold;
                }
                else
                {
                    led.BackColor = Color.Transparent;
                }
            }

            for (int i = 0; i < outputKeyList.Count; i++)
            {
                LED led = ComponentsForModule.OutputControls[outputKeyList[i]];

                if (led.State)
                {
                    led.BackColor = Color.Gold;
                }
                else
                {
                    led.BackColor = Color.Transparent;
                }
            }
        }
    }
}
