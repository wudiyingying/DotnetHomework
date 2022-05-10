using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data.Entity;

namespace Homework6
{
    public class OrderService
    {
        //private List<Order> orders;

        public List<Order> Orders {

            get {
                using (var oc = new OrderContext())
                {
                    return oc.Orders
                        .Include(o => o.OrderDetails.Select(d => d.GoodsItem))
                        .Include("Client")
                        .ToList<Order>();
                }
            }
        }
        
        

        public void addOrder(Order order)
        {
            FixOrder(order);
            using (var oc = new OrderContext())
            {
                oc.Orders.Add(order);
                try {oc.SaveChanges(); }
                catch(Exception ex) 
                { throw; }
                
            }
        }

        public Order GetOrder(string num)
        {
            using (var oc = new OrderContext())
            {
                return oc.Orders
                    .Include(o => o.OrderDetails.Select(d => d.GoodsItem))
                    .Include("Client")
                    .SingleOrDefault(o => o.OrderId == num);
            }
        }

        private static void FixOrder(Order newOrder)
        {
            newOrder.ClientId = newOrder.Client.ID;
            newOrder.Client = null;
            newOrder.OrderDetails.ForEach(d => {
                d.GoodsId = d.GoodsItem.GoodsId;
                d.GoodsItem = null;
            });
        }

        public void deleteOrder(string num)
        {
            var order = GetOrder(num);
            if (order == null) return;
            using (var oc = new OrderContext())
            {
                oc.Orders.Remove(order);
                oc.SaveChanges();
            }
            
        }

   

        public void changeOrder(Order newOrder)
        {
            
            try
            {
                deleteOrder(newOrder.OrderId);
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
                if (order1.TotalPrice > order2.TotalPrice) return 1; 
                else return 0; }
            );
            
        }

        public void sortOrderByNumber()
        {
            Orders.Sort();
        }

    
        public List<Order> queryByClientName(string Name)
        {
            using (var oc = new OrderContext())
            {
                return oc.Orders
                    .Include(o => o.OrderDetails.Select(d => d.GoodsItem))
                    .Include("Client")
                    .Where(o => o.Client.Name == Name)
                    .ToList<Order>();
            }

        }

        public void Export(String fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xs.Serialize(fs, Orders);
            }
        }

        public void Import(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (var oc = new OrderContext())
                {
                    List<Order> temp = (List<Order>)xs.Deserialize(fs);
                    temp.ForEach(order => {
                        if (oc.Orders.SingleOrDefault(o => o.OrderId == order.OrderId) == null)
                        {
                            FixOrder(order);
                            oc.Orders.Add(order);
                        }
                    });
                    oc.SaveChanges();
                }
            }
        }

    }
}
