using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    class Operation:IElement
    {
        private string element;

        public Operation() { }
        public Operation(string str) 
        {
            element = str;
        }
        public Operation(char str)
        {
            element = str.ToString();
        }

        public int getPrior()
        {
            switch (element)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 2;
                case "*": return 3;
                case "/": return 3;
                case "^": return 4;
                default: return 5;  
            }
        }
        public string getElement
        {
            get { return element; }
        }
        public override string ToString()
        {
            return element + getPrior().ToString();
        }
    }
}
