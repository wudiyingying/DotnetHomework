using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{
    
    public class Order:IComparable
    {
        private string OrderID;
        private Client client = new Client();
        private List<OrderDetails> orderDetails;
        private double totalPrice;
        [Key]
        public string orderId{ get => OrderID; set =>OrderID=value; }

        public virtual Client ClientDetail { get => client; set => client = value; }
        
        public List<OrderDetails> OrderDetails { get => orderDetails; set => orderDetails = value; }

        
        //public string ClientName { get => client.name; }

        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public Order(String orderNum,Client c,List<OrderDetails> d)
        {
            orderId = orderNum;
            client = c;
            orderDetails = d;
            totalPrice = getAmount();
        }

        public Order() { 
            
        }

        public Order(Order order)
        {
            this.OrderID = order.OrderID;
            this.client = order.client;
            this.orderDetails = order.orderDetails;
            this.totalPrice = order.totalPrice;
        }


        public double getAmount()
        {
            double n = 0;
            foreach(OrderDetails orderdetail in orderDetails)
            {
                n += orderdetail.getAmount();
            }

            return n;
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            return order!=null&&client.Equals(order.client)&&orderDetails.Equals(order.orderDetails);
        }

        /*
        public override int GetHashCode()
        {

            //return client.GetHashCode() * 10 + orderDetails.GetHashCode() * 20;
        }*/


        public int CompareTo(object obj)
        {
            Order order = obj as Order;
            return this.OrderID.CompareTo(order.OrderID);
        }

        public override string ToString()
        {
            string s = "";
            foreach(OrderDetails orderDetail in orderDetails)
            {
                s += orderDetail.ToString()+"\n";
            }
            return " Order number : "+orderId+"\n Client infoamtion : "+client.ToString()+"\nAmount : "+getAmount()+"\n Order details : \n"+s;
        }
    }
}
