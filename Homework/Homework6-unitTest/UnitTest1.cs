using Homework6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Homework6_unitTest
{
    [TestClass]
    public class UnitTest1
    {
        OrderService orderService;

        [TestInitialize]
        public void initial()
        {
           orderService = new OrderService(OrderService.Import("..\\..\\s.xml"));
        }

        [TestMethod]
        public void queryByClientNameTest()
        {
            string resultName = "XXA";
            Order result=(Order)orderService.queryByClientName("XXA").ToArray().GetValue(0);
            Assert.AreEqual(resultName, result.clientDetail.name);
        }

        [TestMethod]
        public void queryByClientNumTest()
        {
            string resultOrderNum = "A123456";
            Order result = (Order)orderService.queryByOrderNum("000001").ToArray().GetValue(0);
            Assert.AreEqual(resultOrderNum, result.clientDetail.id);
        }

        [TestMethod]
        public void importTest()
        {
            List<Order> temp = OrderService.Import("..\\..\\s.xml");
            Assert.AreEqual(orderService.Orders.Count, temp.Count);
        }

        [TestMethod]
        public void addOrderTest()
        {
            int i = orderService.Orders.Count;
            orderService.addOrder(orderService.Orders.ToArray()[0]);
            Assert.AreEqual(i+1, orderService.Orders.Count);
        }
    }
}
