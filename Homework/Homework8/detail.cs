using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework6;

namespace Homework8
{
    internal class detail
    {
        private OrderDetails details;

        public detail() { }
        public detail(OrderDetails details)
        {
            this.details = details;
        }

        public OrderDetails Details { get => details; set => details = value; }
    }
}
