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

        public List<Order> Orders { get => orders; }
        
        public OrderService(List<Order> orders)
        {
            this.orders = orders;
        }

        public void addOrder(Order order)
        {
            orders.Add(order);
        }

        public void deleteOrder(Order order)
        {
            orders.Remove(order);
        }

        public void changeOrder(Order oldOrder,Order newOrder)
        {
            
            try
            {
                orders.Remove(oldOrder);
                if (orders.Remove(oldOrder) == false) throw new Exception("Change failed : cannot find the order");
                orders.Add(newOrder);
                orders.Sort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

        public void sortOrderByAmount()
        {
            orders.Sort((order1, order2) => { 
                if (order1.getAmount() > order2.getAmount()) return 1; 
                else return 0; }
            );
            
        }

        public void sortOrderByNumber()
        {
            orders.Sort();
        }

        public List<Order> queryByOrderNum(string orderNum)
        {
            try {
                var query = from order in orders where order.orderNum == orderNum select new Order(order);

                if (query.Last() == null) throw new Exception("Select Failed");
                return query.ToList<Order>(); ;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Order>();
            }

            
        }

        public List<Order> queryByClientName(string Name)
        {
            try
            {
                var query = from order in orders where order.clientDetail.name == Name select new Order(order);
                
                
                if (query.Last()==null) throw new Exception("Select Failed");
                return query.ToList<Order>();;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Order>();
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
            XmlSerializer xml = new XmlSerializer(typeof(Order[]));
            List<Order> orders=new List<Order>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fs.Seek(0, SeekOrigin.Begin);
                orders.AddRange((Order[])xml.Deserialize(fs));
            }
            return orders;
        }

    }
}
