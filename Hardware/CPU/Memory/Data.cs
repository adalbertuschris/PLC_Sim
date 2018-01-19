using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CPU.Memory
{
    class Data
    {
        public static Dictionary<string, bool> inputs = new Dictionary<string, bool>();
        public static Dictionary<string, bool> outputs = new Dictionary<string, bool>();
        public static Dictionary<string, bool> markers = new Dictionary<string, bool>();
        public static Dictionary<string, bool> var = new Dictionary<string, bool>();

        public static void Init(Dictionary<string, LED> inputModule, Dictionary<string, LED> outputModule)
        {
            inputs = new Dictionary<string, bool>();
            outputs = new Dictionary<string, bool>();

            List<string> inputKeyList = new List<string>(inputModule.Keys);
            List<string> outputKeyList = new List<string>(outputModule.Keys);

            for (int i = 0; i < inputKeyList.Count; i++)
            {
                inputs.Add(inputKeyList[i], inputModule[inputKeyList[i]].State);
            }

            for (int i = 0; i < outputKeyList.Count; i++)
            {
                outputs.Add(outputKeyList[i], outputModule[outputKeyList[i]].State);
            }
        }
    }
}
