using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Models
{
    public class Stock
    {
        public string MerchantProductNo { get; set; }
        public List<StockLocations> StockLocations { get; set; }
    }
    public class StockLocations
    {
        public Int32 Stock { get; set; }
        public Int32 StockLocationId { get; set; }
    }
}
