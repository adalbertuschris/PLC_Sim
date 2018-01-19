using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hardware.CPU;
using Hardware.CPU.CommandRegister;

namespace Hardware.CPU.CommandRegister
{
    class Operations
    {
        public enum Helper
        {
            set,
            get
        }
        
        private static bool _isLoad;
        private static bool IsLoad
        {
            get
            {
                return _isLoad;
            }
            set
            {
                if (!_isLoad)
                {
                    _isLoad = value;
                }
            }
        }
        private static bool IsLoaded { get; set; }
        private static int blockCounter = 0;
        public static void CreateInstructionList(List<byte[]> networks)
        {
            Memory.Program.InstructionList = new List<Instruction>();
            int counter = 0;
            for (int i = 0; i < networks.Count; i++)
            {
                for (int j = 0; j < networks[i].Length; j += 2)
                {
                    byte firstComponent = networks[i][j];
                    byte secondComponent = networks[i][j + 1];
                    if (firstComponent == 0xba)
                    {
                        Operators operatorIL;
                        if (j == 0)
                        {
                            operatorIL = Operators.BLOCK;
                            if (counter == 0)
                            {
                                DiagnosticData.AddDataSpecial();                                
                            }
                            else
                            {
                                DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.special);
                            }
                        }
                        else
                        {
                            operatorIL = Operators.ANDFUNC;
                            DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.special);
                        }
                        Memory.Program.InstructionList.Add(new CommandRegister.Instruction(operatorIL));
                        IsLoad = false;
                        IsLoaded = false;
                    }
                    else if (firstComponent == 0xbf)
                    {
                        Operators operatorIL = Operators.ENDBLOCK;
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL));
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.special);
                    }
                    else if (firstComponent == 0xfb)
                    {
                        Operators operatorIL = Operators.ORFUNC;
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL));
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.special);
                        IsLoad = false;
                        IsLoaded = false;
                    }
                    else if (firstComponent == 0x41 && secondComponent >= 0x60 && secondComponent <= 0x6f)
                    {
                        Elements operand = new Elements();
                        Operators operatorIL = Operators.HELPERSET;
                        operand.symbol = Symbol.VAR;

                        operand.name = "Var" + (secondComponent - 0x60).ToString();
                        if (!Memory.Data.var.ContainsKey(operand.name))
                        {
                            Memory.Data.var.Add(operand.name, false);
                        }
                        else
                        {
                            Memory.Data.var[operand.name] = false;
                        }
                        //Operators operatorIL = Operators.HELPERSET;
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.special);
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL, operand));
                    }
                    else if (firstComponent == 0x00 && secondComponent >= 0x60 && secondComponent <= 0x6f)
                    {
                        //helper = Helper.get;
                        Elements operand = new Elements();
                        Operators operatorIL = Operators.HELPERGET;
                        operand.symbol = Symbol.VAR;
                        operand.name = "Var" + (secondComponent - 0x60).ToString();
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL, operand));
                    }
                    else if (firstComponent == 0x00 && secondComponent == 0x14)
                    {
                        
                    }
                    else if (firstComponent == 0x10 && secondComponent == 0x66)
                    {
                        Operators operatorIL = Operators.HELPERUP;
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL));
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                        
                    }
                    else if (firstComponent == 0x68 && secondComponent == 0x2d)
                    {
                        Operators operatorIL = Operators.NOT;
                        Memory.Program.InstructionList.Add(new Instruction(operatorIL));
                        DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                    }
                    else
                    {
                        Instruction instruction = CreateInstructionBasedOnElement(firstComponent, secondComponent, counter);
                        if (instruction != null)
                        {
                            Memory.Program.InstructionList.Add(instruction);
                        }
                    }
                    counter += 2;
                }
                IsLoad = false;
                IsLoaded = false;                
            }
        }

        private static Instruction CreateInstructionBasedOnElement(byte firstComponent, byte secondComponent, int counter)
        {
            Elements operand = new Elements();
            Operators operatorIL = Operators.LD;

            //I
            if (firstComponent >= 0xc0 && firstComponent <= 0xcf)
            {
                operand.symbol = Symbol.I;
                if (firstComponent >= 0xc0 && firstComponent <= 0xc7)
                {
                    operatorIL = Operators.AND;
                    operand.name = "I" + secondComponent + "." + (firstComponent - 0xc0).ToString();
                    //System.Diagnostics.Debug.WriteLine("I");
                    IsLoad = true;

                }
                else
                {
                    operatorIL = Operators.OR;
                    operand.name = "I" + secondComponent + "." + (firstComponent - 0xc8).ToString();
                    IsLoad = true;
                }
                DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
            }

            //!I
            else if (firstComponent >= 0xe0 && firstComponent <= 0xef)
            {
                operand.symbol = Symbol.I;
                if (firstComponent >= 0xe0 && firstComponent <= 0xe7)
                {
                    operatorIL = Operators.ANDN;
                    operand.modifier = Modifiers.N;
                    operand.name = "I" + secondComponent + "." + (firstComponent - 0xe0).ToString();
                    //System.Diagnostics.Debug.WriteLine("I");
                    IsLoad = true;
                }
                else
                {
                    operatorIL = Operators.ORN;
                    operand.modifier = Modifiers.N;
                    operand.name = "I" + secondComponent + "." + (firstComponent - 0xe8).ToString();
                    IsLoad = true;
                }
                DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
            }

            //Q
            else if ((firstComponent >= 0xd0 && firstComponent <= 0xdf) || (firstComponent >= 0xf0 && firstComponent <= 0xf7))
            {
                if (firstComponent >= 0xd0 && firstComponent <= 0xd7)
                {
                    operand.symbol = Symbol.Q;
                    operatorIL = Operators.S;
                    operand.name = "Q" + (secondComponent - 0x80).ToString() + "." + (firstComponent - 0xd0).ToString();
                    //System.Diagnostics.Debug.WriteLine("S");
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Set, DiagnosticData.Symbol.standard);
                }
                else if (firstComponent >= 0xd8 && firstComponent <= 0xdf)
                {
                    operand.symbol = Symbol.Q;
                    operatorIL = Operators.ST;
                    operand.name = "Q" + (secondComponent - 0x80).ToString() + "." + (firstComponent - 0xd8).ToString();
                    //System.Diagnostics.Debug.WriteLine("Q");
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Set, DiagnosticData.Symbol.standard);

                }
                else if (firstComponent >= 0xf0 && firstComponent <= 0xf7)
                {
                    operand.symbol = Symbol.Q;
                    operatorIL = Operators.R;
                    operand.name = "Q" + (secondComponent - 0x80).ToString() + "." + (firstComponent - 0xf0).ToString();
                    //System.Diagnostics.Debug.WriteLine("R");
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Reset, DiagnosticData.Symbol.standard);
                }

            }
            //M
            else if ((firstComponent >= 0x80 && firstComponent <= 0x9f) || (firstComponent >= 0xb0 && firstComponent <= 0xbf))
            {
                if (firstComponent >= 0x80 && firstComponent <= 0x87)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.AND;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0x80).ToString();
                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    IsLoad = true;
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                }
                else if (firstComponent >= 0x88 && firstComponent <= 0x8f)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.OR;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0x88).ToString();

                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    IsLoad = true;
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
                }
                else if (firstComponent >= 0x90 && firstComponent <= 0x97)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.S;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0x90).ToString();
                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Set, DiagnosticData.Symbol.standard);
                }
                else if (firstComponent >= 0x98 && firstComponent <= 0x9f)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.ST;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0x98).ToString();
                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Set, DiagnosticData.Symbol.standard);
                }

                else if (firstComponent >= 0xb0 && firstComponent <= 0xb7)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.R;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0xb0).ToString();
                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    DiagnosticData.AddData(counter, DiagnosticData.Operation.Reset, DiagnosticData.Symbol.standard);
                }
            }

            else if ((firstComponent >= 0xa0 && firstComponent <= 0xaf))
            {
                if (firstComponent >= 0xa0 && firstComponent <= 0xa7)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.AND;
                    operand.modifier = Modifiers.N;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0xa0).ToString();
                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    IsLoad = true;
                }
                else if (firstComponent >= 0xa8 && firstComponent <= 0xaf)
                {
                    operand.symbol = Symbol.M;
                    operatorIL = Operators.OR;
                    operand.modifier = Modifiers.N;
                    operand.name = "M" + secondComponent + "." + (firstComponent - 0xa8).ToString();

                    if (!Memory.Data.markers.ContainsKey(operand.name))
                    {
                        Memory.Data.markers.Add(operand.name, false);
                    }
                    IsLoad = true;
                }
                DiagnosticData.AddData(counter, DiagnosticData.Operation.None, DiagnosticData.Symbol.standard);
            }
            else
            {
                return null;
            }

            if (!IsLoaded && IsLoad)
            {
                IsLoaded = true;
                if (operand.modifier == Modifiers.None)
                {
                    operatorIL = Operators.LD;
                }
                else
                {
                    operatorIL = Operators.LDN;
                }
            }
            return new Instruction(operatorIL, operand);
        }
    }
}
