using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        private string id;
        [Key]
        public string Id { get => id; set => id = value; }
        public Goods Goods { get => goods; set => goods = value; }
        public double Discount { get => discount; set => discount = value; }
        public int Count { get => count; set => count = value; }
        public string OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order order { get; set; }


        public OrderDetails(string i,Goods g,double d,int c)
        {
            goods = g;
            discount = d;
            count = c;
            id = i;
        }

        public OrderDetails() { }
        
      
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
