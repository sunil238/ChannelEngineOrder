using ChannelEngineOrder.Models;
using ChannelEngineOrder.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Controllers
{
    public class HomeController : Controller
    {
        IOrderService _orderService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Order()
        {
            ViewData["StockUpdate"] = "Stock is not updated";
            var channelOrders = await _orderService.FetchInprogress();
            ViewData["Orders"] = channelOrders.OrderLines;
            if(channelOrders.IsStockUpdated)
            {
                ViewData["StockUpdate"] = "Stock is successfully updated";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
