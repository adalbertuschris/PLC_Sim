using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hardware;

namespace Symulator_PLC
{
    public class PipeServer
    {
        private static string ConvertAdressPoolAfterReceived(string addressFrom, string address)
        {            
            int dotIndex = addressFrom.IndexOf('.');
            string oryginalKey = addressFrom.Remove(0, 1);
            oryginalKey = oryginalKey.Remove(dotIndex - 1);

            dotIndex = address.IndexOf('.');
            string convertedKey = address.Remove(0,1);
            convertedKey = convertedKey.Remove(dotIndex - 1);

            string key = address[0].ToString() + (Convert.ToInt32(convertedKey) + Convert.ToInt32(oryginalKey)).ToString() + address.Remove(0, dotIndex);
            return key;
        }

        private static string ConvertAdressPoolToSend(string addressFrom, string address)        
        {
            //System.Diagnostics.Debug.WriteLine("addressFrom: " + addressFrom);
            //System.Diagnostics.Debug.WriteLine("address: " + address);
            int dotIndex = addressFrom.IndexOf('.');
            string oryginalKey = addressFrom.Remove(0, 1);
            oryginalKey = oryginalKey.Remove(dotIndex-1);
            //System.Diagnostics.Debug.WriteLine("OryginalKey: " + oryginalKey);
            dotIndex = address.IndexOf('.');
            string convertedKey = address.Remove(0, 1);
            convertedKey = convertedKey.Remove(dotIndex-1);
            //System.Diagnostics.Debug.WriteLine("ConvertedKey: " + convertedKey);
            string key = address[0].ToString() + (Convert.ToInt32(convertedKey) - Convert.ToInt32(oryginalKey)).ToString() + address.Remove(0, dotIndex);
            //System.Diagnostics.Debug.WriteLine("key: " + key);
            return key;
        }

        public static void DataReceivedOperation(string message)
        {
            try
            {
                if (message != "")
                {
                    List<string> keys = new List<string>(PLC.InputModule.Keys);
                    string addressFrom = keys[0];
                    string[] dataReceived = message.Split(',');
                    for (int i = 0; i < message.Length; i += 2)
                    {
                        if (dataReceived[i] == "END")
                        {
                            break;
                        }
                        //Converting address pool from Ix.x to Iy.x
                        string key = ConvertAdressPoolAfterReceived(addressFrom, dataReceived[i]);

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        bool value = Convert.ToBoolean(dataReceived[i + 1]);
                        if (PLC.InputModule.ContainsKey(key))
                        {
                            PLC.InputModule[key].State = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
        }
        static bool IOInitialized = false;
        public static string LoadDataToSend()
        {
            string str = "";
            try
            {
                if (PLC.InputModule != null && PLC.OutputModule != null)
                {
                    List<string> inputKeys = new List<string>(PLC.InputModule.Keys);
                    string inputAddressFrom = inputKeys[0];

                    List<string> outputKeys = new List<string>(PLC.OutputModule.Keys);
                    string outputAddressFrom = outputKeys[0];

                    if (!IOInitialized)
                    {
                        foreach (var input in PLC.InputModule)
                        {
                            string key = ConvertAdressPoolToSend(inputAddressFrom, input.Key);
                            str += key + "," + input.Value.State + ",";
                        }
                        IOInitialized = true;
                    }
                    foreach (var output in PLC.OutputModule)
                    {
                        string key = ConvertAdressPoolToSend(outputAddressFrom, output.Key);
                        str += key + "," + output.Value.State + ",";
                    }
                    str += "END";
                    System.Diagnostics.Debug.WriteLine(str);
                }
                //System.Diagnostics.Debug.WriteLine("Wysłany string: " + str);
            }
            catch { }
            return str;
            
            //string str = "";
            //List<string> keys = new List<string>(IOState.Inputs.Keys);
            //for (int i = 0; i < keys.Count; i++)
            //{
            //    str += keys[i] + "," + IOState.Inputs[keys[i]] + ",";
            //}
        }
    }
}
