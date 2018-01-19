using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Hardware.CPU.CommandRegister;

namespace Hardware.CPU
{
    class Operation
    {
        
        private static int callCounter = 0;//zmienna dla ORFUNC, ORFUNC nie posiada znacznika zamykającego, tylko otwierający
        //potrzebne tylko gdy ORFUNC zostanie wywołane na 0 poziomie networka, w pozostałych wypadkach zmienna ta jest niepotrzebna
        private static bool waitingToSet;
        
        public static void ProgramSweep(List<CommandRegister.Instruction> instructionList, Dictionary<string, LED> inputModule, Dictionary<string, LED> outputModule)
        {
            try
            {
                int commandCounter = 0;
                callCounter = 0;
                waitingToSet = false;
                ReadInputs(inputModule);
                ExecuteProgram(instructionList, ref commandCounter);
                try
                {
                    CPU.DiagnosticData.DiagnosticOperation();
                }
                catch { }
                UpdateOutputs(outputModule);
            }
            catch { }
        }

        

        public static void ReadInputs(Dictionary<string, LED> inputModule)
        {
            List<string> inputKeyList = new List<string>(inputModule.Keys);
            for (int i = 0; i < inputKeyList.Count; i++)
            {
                Memory.Data.inputs[inputKeyList[i]] = inputModule[inputKeyList[i]].State;
            }
        }

        public static void ResetMemory()
        {
            List<string> keyOutputsList = new List<string>(Memory.Data.outputs.Keys);
            for (int i = 0; i < keyOutputsList.Count; i++)
            {
                Memory.Data.outputs[keyOutputsList[i]] = false;
            }

            List<string> keyMarkersList = new List<string>(Memory.Data.markers.Keys);
            for (int i = 0; i < keyMarkersList.Count; i++)
            {
                Memory.Data.markers[keyMarkersList[i]] = false;
            }
        }

        public static void ResetOutputMemory()
        {
            List<string> keyOutputsList = new List<string>(Memory.Data.outputs.Keys);
            for (int i = 0; i < keyOutputsList.Count; i++)
            {
                Memory.Data.outputs[keyOutputsList[i]] = false;
            }
        }

        public static void UpdateOutputs(Dictionary<string, LED> outputModule)
        {
            List<string> keyList = new List<string>(Memory.Data.outputs.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                outputModule[keyList[i]].State = Memory.Data.outputs[keyList[i]];
            }
        }


        static CommandRegister.Elements operand;
        public static bool ExecuteProgram(List<CommandRegister.Instruction> instructionList, ref int i)
        {
            //Debug.WriteLine(i);
            bool state = false;
            bool operandValue;
            while (i != instructionList.Count)
            {
                //Debug.WriteLine(instructionList[i].operatorIL);
                switch (instructionList[i].operatorIL)
                {
                    case Operators.BLOCK:
                        //Debug.WriteLine("BLOCK");
                        i++;
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.special);
                        state = ExecuteProgram(instructionList, ref i);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state);//ENDBLOCK
                        break;
                    case Operators.ENDBLOCK:
                        //Debug.WriteLine("ENDBLOCK");
                        
                        return state;
                    case Operators.LD:
                    //    Debug.WriteLine("LD");
                        state = CheckSymbolAndGetValue(instructionList[i].operand);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state);
                        break;
                    case Operators.LDN:
                        //Debug.WriteLine("LDN");
                        state = !CheckSymbolAndGetValue(instructionList[i].operand);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.standard, DiagnosticData.Modifier.not);
                        //Debug.WriteLine(state);
                        break;
                    case Operators.ORFUNC:
                        //Debug.WriteLine("ORFUNC");
                        i++;
                        callCounter++;
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.special);
                        bool tmp = ExecuteProgram(instructionList, ref i);
                        state = Or(state, tmp);                        
                        callCounter--;
                        
                        if (waitingToSet)
                        {
                            //Debug.WriteLine("wts");
                            i--;
                        }
                        else
                        {
                            //Debug.WriteLine("!wts");
                            return state;
                        }
                        break;
                    case Operators.OR:
                        //Debug.WriteLine("OR");
                        operandValue = CheckSymbolAndGetValue(instructionList[i].operand);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, operandValue);
                        state = Or(state, operandValue);
                        
                        break;
                    case Operators.ORN:
                        //Debug.WriteLine("ORN");
                        operandValue = !CheckSymbolAndGetValue(instructionList[i].operand);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, operandValue, DiagnosticData.Symbol.standard, DiagnosticData.Modifier.not);
                        state = Or(state, operandValue);
                        break;
                    case Operators.AND:
                        //Debug.WriteLine("AND");
                        operandValue = CheckSymbolAndGetValue(instructionList[i].operand);                        
                        state = And(state, operandValue);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state);
                        break;
                    case Operators.ANDN:
                        //Debug.WriteLine("ANDN");
                        operandValue = !CheckSymbolAndGetValue(instructionList[i].operand);
                        state = And(state, operandValue);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.standard, DiagnosticData.Modifier.not);
                        break;
                    case Operators.ANDFUNC:
                        //Debug.WriteLine("ANDFUNC");
                        i++;
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.special);
                        bool tmpAnd = ExecuteProgram(instructionList, ref i);
                        state = And(state, tmpAnd);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state);//ENDBLOCK
                        //return state;
                        break;
                    case Operators.HELPERGET:
                        operandValue = CheckSymbolAndGetValue(instructionList[i].operand);                        
                        state = operandValue;
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state);
                        break;
                    case Operators.HELPERSET:
                        operand = instructionList[i].operand;
                        CheckSymbolAndSetValue(operand, state);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.special);
                        break;
                    case Operators.HELPERUP:
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.standard);
                        break;
                    case Operators.NOT:
                        state = !state;
                        DiagnosticData.RefreshState(DiagnosticData.Operation.None, state, DiagnosticData.Symbol.standard);
                        //DiagnosticData.AddData(state, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                        break;
                    case Operators.R:
                        //return state;
                        //Debug.WriteLine("R");
                        if (callCounter == 1)
                        {
                            waitingToSet = true;
                            return state;
                        }
                        operandValue = CheckSymbolAndGetValue(instructionList[i].operand);
                        bool finalValue;
                        if (state)
                        {
                            finalValue = false;
                        }
                        else
                        {
                            finalValue = operandValue;
                        }
                        operand = instructionList[i].operand;
                        CheckSymbolAndSetValue(operand, finalValue);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.Reset, state);
                        break;
                    case Operators.S:
                        if (callCounter == 1)
                        {
                            waitingToSet = true;
                            return state;
                        }
                        //Debug.WriteLine("S");
                        //return state;
                        bool finalValue2;
                        operandValue = CheckSymbolAndGetValue(instructionList[i].operand);
                        if (state)
                        {
                            finalValue2 = true;
                        }
                        else
                        {
                            finalValue2 = operandValue;
                        }
                        operand = instructionList[i].operand;
                        CheckSymbolAndSetValue(operand, finalValue2);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.Set, state);
                        break;
                    case Operators.ST:
                        if (callCounter == 1)
                        {
                            waitingToSet = true;
                            return state;
                        }
                        //Debug.WriteLine("ST");
                        operand = instructionList[i].operand;
                        CheckSymbolAndSetValue(operand, state);
                        DiagnosticData.RefreshState(DiagnosticData.Operation.Set, state);
                        //Debug.WriteLine(state);
                        break;
                    //return state;
                }
                i++;
            }            

            return state;
        }

        public static bool CheckSymbolAndGetValue(CommandRegister.Elements element)
        {
            bool value = false;
            if (element.symbol == Symbol.I)
            {
                value = Memory.Data.inputs[element.name];
            }
            else if (element.symbol == Symbol.M)
            {
                value = Memory.Data.markers[element.name];
            }
            else if (element.symbol == Symbol.Q)
            {
                value = Memory.Data.outputs[element.name];
            }
            else if (element.symbol == Symbol.VAR)
            {
                value = Memory.Data.var[element.name];
            }

            return value;
        }

        public static void CheckSymbolAndSetValue(CommandRegister.Elements element, bool value)
        {
            if (element.symbol == Symbol.M)
            {
                Memory.Data.markers[element.name] = value;
            }
            else if (element.symbol == Symbol.Q)
            {
                Memory.Data.outputs[element.name] = value;
            }
            else if (element.symbol == Symbol.VAR)
            {
                Memory.Data.var[element.name] = value;
            }
        }

        public static bool Or(bool first, bool second)
        {
            if (first || second)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool And(bool first, bool second)
        {
            if (first && second)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
    }
}
