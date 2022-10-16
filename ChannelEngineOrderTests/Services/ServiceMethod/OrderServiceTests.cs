using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelEngineOrder.Services.ServiceMethod;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using ChannelEngineOrder.Models;
using Xunit;
using ChannelEngineOrder.Services.Interfaces;
using Moq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChannelEngineOrder.Services.ServiceMethod.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        private readonly IOrderService _orderServiceMock;
        public OrderServiceTests()
        {
            _orderServiceMock = new OrderService();
        }
        [TestMethod]
        public async Task FetchInprogressFetchInProgressTest()
        {
            List<OrderLines> orderLinesLocal = new List<OrderLines>();
            orderLinesLocal.Add(new OrderLines
            {
                Gtin = "8719351029609",
                Description = "T-shirt met lange mouw BASIC petrol: S",
                Quantity = 4
            });
            orderLinesLocal.Add(new OrderLines
            {
                Gtin = "8719351029609",
                Description = "T-shirt met lange mouw BASIC petrol: M",
                Quantity = 4
            });
            orderLinesLocal.Add(new OrderLines
            {
                Gtin = "8719351029609",
                Description = "T-shirt met lange mouw BASIC petrol: XL",
                Quantity = 1
            });
            orderLinesLocal.Add(new OrderLines
            {
                Gtin = "8719351029609",
                Description = "T-shirt met lange mouw BASIC petrol: L",
                Quantity = 1
            });
            var expectedOrderResult = new OrderLinesStockUpdate
            {
                IsStockUpdated = true,
                OrderLines = orderLinesLocal
            };
            var actualOrderResult =await _orderServiceMock.FetchInprogress();
            Assert.AreEqual(JsonConvert.SerializeObject(expectedOrderResult), JsonConvert.SerializeObject(actualOrderResult));
        }
    }
}