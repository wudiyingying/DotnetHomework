using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework6;

namespace Homework8
{
    internal class NowOrders
    { 
        
        
       
        private string orderNum;
        private string clientName;
        private string clientNum;

        private string OrderDetails;
        
        

        public string OrderNum { get => orderNum; set => orderNum = value; }
        public string ClientName { get => clientName; set => clientName = value; }
        public string ClientNum { get => clientNum; set => clientNum = value; }
        public string OrderDetails1 { get => OrderDetails; set => OrderDetails = value; }

        public NowOrders() {
           
        }

        public NowOrders(string orderNum, string clientName, string clientNum, string orderDetails1)
        {
            OrderNum = orderNum;
            ClientName = clientName;
            ClientNum = clientNum;
            OrderDetails1 = orderDetails1;
        }


        //private OrderService orderService = new OrderService(nowOrder);
    }
}
