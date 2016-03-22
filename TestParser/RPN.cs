using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    /// <summary>
    /// Работа с обратной польской нотацией.
    /// </summary>
    class RPN
    {
        /// <summary>
        /// Начальная строка в инфиксной форме
        /// </summary>
        public string beginString { get; set; }
        /// <summary>
        /// Стек входной строки, разделенной на элементы
        /// </summary>
        private Stack<IElement> input = new Stack<IElement>();
        /// <summary>
        /// RPN поэлементно в стеке.
        /// </summary>
        public Stack<IElement> istack { get; set; }
        public RPN() { }
        public RPN(string str)
        {
            beginString = str;
            parseRPN();
        }


        private bool isOperation(char c)
        {
            return c == '-' || c == '+' || c == '*' || c == '/' || c == '\\' || c == '\\' || c == '(' || c == ')' || c == '^';
        }
        private bool isDigit(char c)
        {
            return (Char.IsDigit(c)||c==',');
        }
        private bool isVariable(char c)
        {
            return c == 'x' || c == 'X' || c == 'х' || c == 'Х';
        }
        /// <summary>
        /// Преобразование строки в RPN
        /// </summary>
        private void parseRPN()
        {
            Stack<IElement> input = new Stack<IElement>();
            Stack<IElement> reverse = new Stack<IElement>();
            int i = 0;
            string temp = "";
            while (i <= beginString.Length - 1)
            {
                if (isOperation(beginString[i]))
                {
                    input.Push(new Operation(beginString[i]));
                    i++;
                    continue;
                }
                if (isDigit(beginString[i]))
                {
                    temp = "";
                    while ((i <= beginString.Length - 1) && (isDigit(beginString[i])))
                    {
                        temp += beginString[i];
                        i++;
                    }
                    input.Push(new Number(temp));
                    continue;
                }
                if (isVariable(beginString[i]))
                {
                    input.Push(new Variable(beginString[i]));
                    i++;
                    continue;
                }
                i++;
            }
            foreach (IElement n in input)
            {
                reverse.Push(n);
            }
            istack = stackToRPNStack(reverse);
        }
        
        /// <summary>
        /// Преобразование стека с инфиксной записью в стек с постфиксной
        /// </summary>
        private Stack<IElement> stackToRPNStack(Stack<IElement> input)
        {
            int counter = 0;
            Stack<Operation> stack = new Stack<Operation>();
            Stack<IElement> output = new Stack<IElement>();
            foreach (IElement IE in input)
            {
                counter++;
                if (IE.GetType() != typeof(Operation)) output.Push(IE);
                else
                {
                    if (stack.Count == 0) stack.Push((Operation)IE);
                    else if (((Operation)IE).getPrior() >= 2)
                    {
                        while (stack.Count != 0 && ((Operation)IE).getPrior() <= stack.Peek().getPrior()) output.Push(stack.Pop());
                        stack.Push((Operation)IE);
                    }
                    else if (((Operation)IE).getPrior() == 0) stack.Push((Operation)IE);
                    else// закрывающая скобка
                    {
                        while (stack.Count != 0 && stack.Peek().getPrior() != 0) output.Push(stack.Pop());
                        stack.Pop();
                    }
                }
            }
            while (stack.Count != 0) output.Push(stack.Pop());
            return output;
        }

    }
}
