using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CPU.CommandRegister
{
    public enum Operators
    {
        LD,
        LDN,
        NOT,
        OR,
        ORN,
        ORFUNC,
        AND,
        ANDN,
        ANDFUNC,
        BLOCK,
        ENDBLOCK,
        HELPERSET,
        HELPERGET,
        HELPERUP,
        ST,
        S,
        R
    }

    class Instruction
    {
        public Operators operatorIL { get; set; }
        public Elements operand { get; set; }

        public Instruction(Operators operatorIL, Elements operand = null)
        {
            this.operatorIL = operatorIL;
            this.operand = operand;
        }
    }
}
