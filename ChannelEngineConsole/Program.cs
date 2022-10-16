using ChannelEngineOrder.Services.Interfaces;
using ChannelEngineOrder.Services.ServiceMethod;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ChannelEngineConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IOrderService, OrderService>()
            .BuildServiceProvider();
            var orderService = serviceProvider.GetService<IOrderService>();
            var results = await orderService.FetchInprogress();
            foreach(var result in results.OrderLines)
            {
                Console.WriteLine("Gtin : " + result.Gtin + "\t" + "Description : " + result.Description + "\t" + "Quantity : " + result.Quantity + "\n");
            }
           var stockUpdateMessage =  results.IsStockUpdated==true ? "Stock is Updated Successfully" : "Stock not Updated";

            Console.WriteLine(stockUpdateMessage);

        }
    }
}
