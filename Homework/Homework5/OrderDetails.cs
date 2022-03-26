using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{
  
    public class OrderDetails
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

        public OrderDetails() { }

        public Goods goodsDetail { get => goods; set => goods = value; }

        public double Discount { get => discount; set => discount = value; }

        public int Count { get => count; set => count = value; }
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
