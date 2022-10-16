using ChannelEngineOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderLinesStockUpdate> FetchInprogress();
    }
}
