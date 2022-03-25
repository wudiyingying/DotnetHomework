using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    [Serializable]
    internal class Order:IComparable
    {
        private string OrderNumber;
        private Client client;
        private List<OrderDetails> orderDetails;
 

        public string orderNum{ get => OrderNumber; }
        public string clientName { get => client.name; }

        public Order(String orderNum,Client c,List<OrderDetails> d)
        {
            OrderNumber = orderNum;
            client = c;
            orderDetails = d;
        }

        public Order(Order order)
        {
            this.OrderNumber = order.OrderNumber;
            this.client = order.client;
            this.orderDetails = order.orderDetails;
            
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
            return client.Equals(order.client)&&orderDetails.Equals(order.orderDetails);
        }

        public override int GetHashCode()
        {
            return client.GetHashCode()*10+orderDetails.GetHashCode()*20;
        }

        public int CompareTo(object obj)
        {
            Order order = obj as Order;
            return this.OrderNumber.CompareTo(order.OrderNumber);
        }

        public override string ToString()
        {
            string s = "";
            foreach(OrderDetails orderDetail in orderDetails)
            {
                s += orderDetail.ToString()+"\n";
            }
            return " Order number : "+orderNum+"\n Client infoamtion : "+client.ToString()+"\nAmount : "+getAmount()+"\n Order details : \n"+s;
        }
    }
}
