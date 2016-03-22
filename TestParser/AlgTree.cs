using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    /// <summary>
    /// Класс для работы с арифметическим деревом
    /// </summary>
    class AlgTree
    {
        public Node root { get; set; }

        public AlgTree() { }
        public AlgTree(Node root)
        {
            this.root = root;
        }
        /// <summary>
        /// Результат операции
        /// </summary>
        /// <param name="ie1">Первый операнд</param>
        /// <param name="op">Математическая операция</param>
        /// <param name="ie2">Второй операнд</param>
        /// <returns>Резальтат математической операции</returns>
        private double result(IElement ie1, Operation op, IElement ie2)
        {
            double num1 = Double.Parse(ie1.getElement);
            double num2 = Double.Parse(ie2.getElement);
            switch (op.getElement)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "/":
                    {
                        if (num2 != 0) { return num1 / num2; }
                        else 
                        {
                            return 0;
                            //throw new DivideByZeroException("Деление на ноль!");
                        } 
                    }
                case "*": return num1 * num2;
                case "^": return Math.Pow(num1, num2);
                default: return 0;
            }
        }
        /// <summary>
        /// Преобразование RPN в арифметическое дерево
        /// </summary>
        /// <param name="ie">Стек с постфиксной записью</param>
        /// <returns>Корень арифметического дерева</returns>
        public Node alg(Stack<IElement> ie)
        {
            if (ie.Count != 0)
            {

                IElement temp = ie.Pop();
                Node nod = new Node(temp);
                if (temp.GetType() != typeof(Operation))
                {
                    return nod;
                }
                else
                {
                    nod.Right = alg(ie);
                    nod.Left = alg(ie);
                    return nod;
                }
            }
            return new Node(ie.Pop());
        }
        /// <summary>
        /// Вычисляет арифметическое дерево
        /// </summary>
        /// <param name="nd">Корень дерева</param>
        /// <param name="x">Аргумент</param>
        /// <returns>Результат вычисления дерева</returns>
        public double alg2(Node nd, double x)
        {
            if (nd != null)
            {
                if (nd.Data.GetType() != typeof(Operation))
                {
                    if (nd.Data.GetType() == typeof(Number))
                        return ((Number)nd.Data).getNum;
                    else return x;
                }
                if ((nd.Data.GetType() == typeof(Operation)) && (nd.Left.Data.GetType() != typeof(Operation)) && (nd.Right.Data.GetType() != typeof(Operation)))
                {
                    if ((nd.Left.Data.GetType() == typeof(Number)) && (nd.Right.Data.GetType() == typeof(Number)))
                        return result(nd.Left.Data, (Operation)nd.Data, nd.Right.Data);
                    else if (nd.Left.Data.GetType() == typeof(Number))
                    {
                        return result(nd.Left.Data, (Operation)nd.Data,new Number(x));
                    }
                    else
                        if((nd.Right.Data.GetType() != typeof(Number)))
                        {
                            return result(new Number(x), (Operation)nd.Data, new Number(x));
                        }
                        else
                        return result(new Number(x), (Operation)nd.Data, nd.Right.Data);
                }
                else
                {
                    if ((nd.Left != null) || (nd.Right != null))
                    {
                        if ((nd.Left.Data.GetType() == typeof(Operation)) ^ (nd.Right.Data.GetType() == typeof(Operation)))
                            if (nd.Left.Data.GetType() == typeof(Operation))
                            {
                                if (nd.Right.Data.GetType() == typeof(Variable))
                                {
                                    return result(new Number(alg2(nd.Left, x)), (Operation)nd.Data,new Number(x));
                                }
                                else return result(new Number(alg2(nd.Left, x)), (Operation)nd.Data, nd.Right.Data);
                            }
                            else
                            {
                                if (nd.Left.Data.GetType() == typeof(Variable))
                                {
                                    return result(new Number(x), (Operation)nd.Data, new Number(alg2(nd.Right, x))); ;
                                }
                                else return result(nd.Left.Data, (Operation)nd.Data, new Number(alg2(nd.Right, x)));
                            }
                        else
                        {
                            return result(new Number(alg2(nd.Left, x)), (Operation)nd.Data, new Number(alg2(nd.Right, x)));
                            //alg2(nd.Left, x);
                            //alg2(nd.Right, x);
                        }
                    }
                }

            }
            return 0;
        }
    }
}
