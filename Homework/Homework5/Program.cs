using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Homework6
{
    internal class Program
    {

        //这个main里面只是做了方法实现的演示，其余的输入输出文件读写以省略
        static void Main(string[] args)
        {
           
            Client client1 = new Client("XXA","A123456");
            Client client2 = new Client("XXB","A123457");

            List<Goods> goods = new List<Goods>
            {
                new Goods("EarPhone","EA001",1000),
                new Goods("Phone","EA005",5000),
                new Goods("Laptop","EA010",8000),
                new Goods("Keybord","EA109",500),
                new Goods("Displayer","EA002",2000),
                new Goods("TabletPC","EA221",6000)
            };

            List<Order> orders = new List<Order>{
                new Order("XX0001", client1),
                new Order("XX0002", client2)

            };

            List<OrderDetails> details1 = new List<OrderDetails>
            {
                new OrderDetails(1,goods[0],2),
                new OrderDetails(2,goods[3],3),
                new OrderDetails(3,goods[4],1),
                new OrderDetails(4,goods[5],1),
            };

            List<OrderDetails> details2 = new List<OrderDetails>
            {
                new OrderDetails(1,goods[2],2),
                new OrderDetails(2,goods[1],3),
                new OrderDetails(3,goods[5],1),
                new OrderDetails(4,goods[3],1),
            };
            

            orders[0].AddDetails(details1);
            orders[1].AddDetails(details2);

            OrderService os = new OrderService();
            os.addOrder(orders[0]);
            //os.addOrder(orders[1]);


            /*
            List<OrderDetails> details1 = new List<OrderDetails>
            {
                new OrderDetails(orders[0],goods[0],0.9,2),
                new OrderDetails(orders[0],goods[3],0.95,3),
                new OrderDetails(orders[0],goods[4],0.8,1),
                new OrderDetails(orders[0],goods[5],0.88,1),
            };

            List<OrderDetails> details2 = new List<OrderDetails>
            {
                new OrderDetails(orders[1],goods[2],0.8,2),
                new OrderDetails(orders[1],goods[1],0.95,3),
                new OrderDetails(orders[1],goods[5],0.8,1),
                new OrderDetails(orders[1],goods[4],0.88,1),
            };
            
            orders[0].AddDetails(details1);
            orders[1].AddDetails(details2);

            using (var oc=new OrderContext())
            {
                oc.Goods.Add(goods[0]);
                oc.Goods.Add(goods[1]);
                oc.Goods.Add(goods[2]);
                oc.Goods.Add(goods[3]);
                oc.Goods.Add(goods[4]);
                oc.Goods.Add(goods[5]);
                oc.Clients.Add(client1);
                oc.Clients.Add(client2);
                oc.SaveChanges();
            }
            */



            /*
            Order[] orders = {
                new Order("000001", client1, details1),
                new Order("000002", client2, details2)};
            */



            //List<Order> testOrder=OrderService.Import("..\\..\\s.xml");



            /*
            Console.WriteLine("The first order ");
            Console.WriteLine(orders[0]);
            Console.WriteLine("\nThe second order ");
            Console.WriteLine(orders[1]);

            Console.WriteLine("add an order to the orders,using addOrder()");
            orderService.addOrder(new Order("000003", client1, details3));
            Console.WriteLine(orders[2]);

            Console.WriteLine("Sort orders by amount ,using sortOrderByAmount():");
            orderService.sortOrderByAmount();
            foreach(Order order in orders)
            {
       
                Console.WriteLine(order);
                
            }

            Console.WriteLine("Sort orders by number ,using sortOrderByNumber():");
            orderService.sortOrderByNumber();
            foreach (Order order in orders)
            {
                Console.WriteLine(order);
            }

            Console.WriteLine("Query order by client name 'XXA',using queryByClientName()");
            
            foreach (Order order in orderService.queryByClientName("XXA"))
            {
                Console.WriteLine(order);
            }

            Console.WriteLine("Query order by client name 'XXV',Can not find the order ");
            foreach (Order order in orderService.queryByClientName("XXV"))
            {
                Console.WriteLine(order);
            }
            */
        }
    }
}
