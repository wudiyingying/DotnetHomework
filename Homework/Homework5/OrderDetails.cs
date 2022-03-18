using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    internal class OrderDetails
    {
        private Goods goods;
        private double discount;
        private int count;

        public OrderDetails(Goods g,double d,int c)
        {
            goods = g;
            discount = d;
            count = c;
        }
        public override string ToString()
        {
            return goods.ToString()+" Discount : "+discount +" Count : "+count;
        }

        public double getAmount()
        {
            return goods.price*discount*count;
        }
    }
}
