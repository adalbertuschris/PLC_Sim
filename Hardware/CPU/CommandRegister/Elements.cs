using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware.CPU.CommandRegister
{
    public enum Symbol
    {
        I,
        Q,
        M,
        VAR
    }

    public enum Modifiers
    {
        N,
        None
    }

    class Elements
    {
        public Symbol symbol;
        public Modifiers modifier = Modifiers.None;
        //public bool value;
        public string name;
    }
}
