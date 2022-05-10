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
        
       
        public string OrderId{ get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }
        
        public List<OrderDetails> OrderDetails { get; set; }

        
        public string ClientName { get => Client != null ? Client.Name : ""; }

        //public string ClientName { get => client.name; }

        public double TotalPrice
        {
            get => OrderDetails.Sum(item => item.TotalPrice);
        }
        public Order(String orderNum,Client c)
        {
            OrderId = orderNum;
            ClientId = c.ID;
            Client = c;
            OrderDetails = new List<OrderDetails>();
            
        }

        public Order(string orderNum,Client c,List<OrderDetails> ods)
        {
            OrderId = orderNum;
            ClientId = c.ID;
            Client = c;
            OrderDetails = ods;
            
        }

        public void AddDetails(List<OrderDetails> ods)
        {
            ods.ForEach(o =>
            {
                if (!OrderDetails.Contains(o)) OrderDetails.Add(o); ;

            });
           
        }

        public void AddDetail(OrderDetails ods)
        {
            if (OrderDetails.Contains(ods)) return;
            OrderDetails.Add(ods);
        }
        
        public void RemoveDetails(OrderDetails ods)
        {
            OrderDetails.Remove(ods);

        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            return order!=null&&Client.Equals(order.Client)&&OrderDetails.Equals(order.OrderDetails);
        }

        public override int GetHashCode()
        {
            var hashCode = -531220479;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientName);
           
            return hashCode;
        }

        public int CompareTo(object obj)
        {
            Order order = obj as Order;
            return this.OrderId.CompareTo(order.OrderId);
        }

        public override string ToString()
        {
            string s = "";
            foreach(OrderDetails orderDetail in OrderDetails)
            {
                s += orderDetail.ToString()+"\n";
            }
            return " Order number : "+OrderId+"\n Client infoamtion : "+Client.ToString()+"\nAmount : "+TotalPrice+"\n Order details : \n"+s;
        }
    }
}
