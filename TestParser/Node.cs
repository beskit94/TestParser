using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestParser
{
    /// <summary>
    /// Узел
    /// </summary>
    class Node
    {
        /// <summary>
        /// Данные в узле
        /// </summary>
        public IElement Data { get; set; }
        /// <summary>
        /// Левый потомок
        /// </summary>
        public Node Left { get; set; }
        /// <summary>
        /// Правый потомок
        /// </summary>
        public Node Right { get; set; }

        public Node(IElement ie)
        {
            this.Data = ie;
        }
        public Node()
        {
        }
    }
}
