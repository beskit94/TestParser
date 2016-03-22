using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    class Variable: IElement
    {
        private string element;
        public Variable() { }
        public Variable(string str) 
        {
            element = str;
        }
        public Variable(char str)
        {
            element = str.ToString();
        }

        public string getElement
        {
            get { return element; }
        }
        public override string ToString()
        {
            return element;
        }
    }
}
