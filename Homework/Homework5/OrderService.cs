using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Homework6
{
    public class OrderService
    {
        private List<Order> orders;

        public List<Order> Orders {

            get {
                using (var oc = new OrderContext())
                {
                    return oc.Orders.ToList<Order>();
                }
            }
        }
        
        public OrderService(List<Order> orders)
        {
            this.orders = orders;
        }

        public void addOrder(Order order)
        {
            if (Orders.Contains(order)) throw new Exception("订单已经存在了!");
            using (var oc = new OrderContext())
            {
                oc.Orders.Add(order);
                oc.SaveChanges();
            }
        }

        public Order GetOrder(string num)
        {
            using (var oc = new OrderContext())
            {
                var query = oc.Orders.Include("Orderdetails").Include("Client").SingleOrDefault(o => o.orderId == num);
                if (query != null) return query;
                else return null;
            }
        }

        public void deleteOrder(Order order)
        {
           
            using (var oc = new OrderContext())
            {
                oc.Orders.Remove(order);
                oc.SaveChanges();
            }
            
        }

        public void deleteOrderByNum(string num) {
            Order order = GetOrder(num);
            if (order != null)
            {
                using (var oc = new OrderContext())
                {
                    oc.Orders.Remove(order);
                    oc.SaveChanges();
                }
            }
        }

        public void changeOrder(Order oldOrder,Order newOrder)
        {
            
            try
            {
                deleteOrder(oldOrder);
                addOrder(newOrder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

        public void sortOrderByAmount()
        {
            Orders.Sort((order1, order2) => { 
                if (order1.getAmount() > order2.getAmount()) return 1; 
                else return 0; }
            );
            
        }

        public void sortOrderByNumber()
        {
            Orders.Sort();
        }

        public List<Order> queryByOrderNum(string orderNum)
        {
            using (var oc = new OrderContext())
            {
                var query = oc.Orders.Include("Orderdetails").Include("Client").Where(o=>o.orderId==orderNum);
                if (query != null) return query as List<Order>;
                else return null;
            }


        }

        public List<Order> queryByClientName(string Name)
        {
            using (var oc = new OrderContext())
            {
                var query = oc.Orders.Include("Orderdetails").Include("Client").Where(o => o.ClientDetail.name == Name);
                if (query != null) return query as List<Order>;
                else return null;
            }

        }

        public void Export()
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(Order[]));
            using (FileStream fs = new FileStream(@"..\..\s.xml", FileMode.Create))
            {
                xml.Serialize(fs, orders.ToArray());
              
            }

        }

        public static List<Order> Import(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            List<Order> orders=new List<Order>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fs.Seek(0, SeekOrigin.Begin);
                orders.AddRange((List<Order>)xml.Deserialize(fs));
            }
            return orders;
        }

    }
}
