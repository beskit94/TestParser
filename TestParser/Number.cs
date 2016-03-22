using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    class Number:IElement
    {
        private string element;
        private double num;
        public Number() { }
        public Number(string str) 
        {
            element = str;
            num = Double.Parse(str);
        }
        public Number(double num)
        {
            this.num = num;
            element = num.ToString();
        }
        public double getNum
        {
            get { return num; }
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
