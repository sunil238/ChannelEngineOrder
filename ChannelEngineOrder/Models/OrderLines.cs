using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Models
{
    public class OrderLinesWithStock
    {
        public string Gtin { get; set; }
        public string Description { get; set; }
        public Int32 Quantity { get; set; }
        public string MerchantProductNo { get; set; }
        public StockLocation StockLocation { get; set; }

    }
    public class OrderLinesStockUpdate
    {
        public List<OrderLines> OrderLines { get; set; }
        public bool IsStockUpdated { get; set; }
    }
    public class OrderLines
    {
        public string Gtin { get; set; }
        public string Description { get; set; }
        public Int32 Quantity { get; set; }
    }
}
