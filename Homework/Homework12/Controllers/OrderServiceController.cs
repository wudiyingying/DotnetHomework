using Homework12.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework12.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderServiceController
    {
        private readonly OrderContext orderDb;

        public OrderServiceController(OrderContext context)
        {
            this.orderDb = context;
        }

        //添加订单
        [HttpPost]
        public ActionResult<Order> PostOrder(Order o)
        {
            o.Customer = null;
            o.Items.ForEach(i => i.GoodsItem = null);
            orderDb.Orders.Add(o);
            orderDb.SaveChanges();
            return o;
        }

       
        [HttpDelete("{OrderId}")]
        public ActionResult<Order> DeleteOrder(string id)
        {
            Order delete = orderDb.Orders.Include(o => o.Items).FirstOrDefault(o => o.OrderId == id);

            if (delete != null)
            {
                orderDb.Orders.Remove(delete);
                orderDb.SaveChanges();
            }
            return NoContent();
        }

        [HttpPut("{OrderId}")]
        public ActionResult<Order> PutOrder(string id, Order updateOrder)
        {
           
            var oldItems = orderDb.OrderItems.Where(i => i.OrderId == id);
            orderDb.OrderItems.RemoveRange(oldItems);
          
            updateOrder.Items.ForEach(i => i.GoodsItem = null);
            orderDb.OrderItems.AddRange(updateOrder.Items);
          
            orderDb.Entry(updateOrder).State = EntityState.Modified;
            orderDb.SaveChanges();
            return NoContent();
        }

        
        [HttpGet]
        public ActionResult<List<Order>> GetOrders(string customer, string Item)
        {
            IQueryable<Order> query = BuildQuery(customer, Item);
            List<Order> orderList = query.ToList();
            orderList.Sort();
            return orderList;
        }

        [HttpGet("{OrderId}")]
        public ActionResult<Order> SelectOrdersByOrderCode(string id)
        {
            Order selectedOrder = orderDb.Orders.Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Id).
                                    FirstOrDefault(o => o.OrderId == id);
            if (selectedOrder == null)
            {
                return NotFound();
            }
            return selectedOrder;
        }

        //构造Query
        public IQueryable<Order> BuildQuery(string customer, string Item)
        {

            IQueryable<Order> query = orderDb.Orders.Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Id).
                                        Where(o => o.OrderId != null);
            if (customer != null)
            {
                query = query.Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Id).
                            Where(o => o.Customer == customer);
            }
            if (Item != null)
            {
                query = query.Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Id).
                            Where(o => o.Items.Any(i => i.ItemName == Item));
            }

            return query;
        }
    }
}
