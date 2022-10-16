using ChannelEngineOrder.Models;
using ChannelEngineOrder.Services.Interfaces;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Services.ServiceMethod
{
    public class OrderService: IOrderService
    {
        string baseUri = "https://api-dev.channelengine.net/";
        JsonSerializerSettings DeserializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            ContractResolver = new ReadOnlyJsonContractResolver(),
            Converters = new List<JsonConverter>
                { new Iso8601TimeSpanConverter()}
        };
        string apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        /// <summary>
        /// Method to fetch top 5 product based on quntity
        /// </summary>
        /// <returns></returns>
        public async Task<OrderLinesStockUpdate> FetchInprogress()
        {
            var endPoint = "api/v2/orders";
            var status = "IN_PROGRESS";
            var URL = baseUri + endPoint + "?statuses=" + status + "&apikey=" + apiKey;
            var _httpRequest = new HttpRequestMessage();
            var httpClient = new HttpClient();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(URL);
            string _responseContent = null;
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            _httpResponse = await httpClient.SendAsync(_httpRequest);
            if(Convert.ToInt32(_httpResponse.StatusCode) == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync();
                _result.Body = JsonConvert.DeserializeObject<ChannelOrder>(_responseContent, DeserializerSettings);
            }
            var channelObject = _result.Body as ChannelOrder;
            List<Content> channelContent = channelObject.Content;
            var channelContentOnlyOrderLine= channelContent.Select(x => x.Lines).ToList();
            var channelContentOnlyOrderLineConverted = ConvertLines(channelContentOnlyOrderLine).ToList();
            var channelContentGrouped = channelContentOnlyOrderLineConverted.GroupBy(x => new { x.MerchantProductNo }).Select(y => new OrderLinesWithStock
            {//here group by was done based on MerchantProductNo because all the 6 in progress order has same GTIN
                Gtin = y.First().Gtin,
                Description = y.First().Description,
                Quantity = y.Sum(k => k.Quantity),
                MerchantProductNo = y.First().MerchantProductNo,
                StockLocation = y.First().StockLocation
            }).ToList();
            var channelContentTopX = channelContentGrouped.OrderByDescending(x => x.Quantity).Take(5).ToList();
            var channelContentTop1Prod = channelContentTopX.First();
            List<StockLocations> stockLocation1 = new List<StockLocations>();
            stockLocation1.Add(new StockLocations
            {
                Stock = 25,
                StockLocationId = channelContentTop1Prod.StockLocation.Id,
            });
            var channelContentTop1 = new Stock
            {
                MerchantProductNo = channelContentTop1Prod.MerchantProductNo,
                StockLocations = stockLocation1

            };
            List<Stock> channelContentTop1s = new List<Stock>();
            channelContentTop1s.Add(channelContentTop1);
            var StockUpdateResult =await UpdateStock(channelContentTop1s);
            var channelContentTopXProd = new OrderLinesStockUpdate
            {
                IsStockUpdated = StockUpdateResult,
                OrderLines = channelContentTopX.Select(y => new OrderLines
                {
                    Gtin = y.Gtin,
                    Description = y.Description,
                    Quantity = y.Quantity
                }).ToList()
            };
            return channelContentTopXProd;
        }
        /// <summary>
        /// Used to conver List of line items inside each order to a single list
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public List<OrderLinesWithStock> ConvertLines(List<List<Lines>> lines)
        {
            List<OrderLinesWithStock> orderLineswitStock = new List<OrderLinesWithStock>() ;
            OrderLinesWithStock orderLinewitStock = new OrderLinesWithStock();
            foreach (var lineitems in lines)
            {
                orderLineswitStock.AddRange(lineitems.Select(x => new OrderLinesWithStock
                {
                    Gtin = x.Gtin,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    MerchantProductNo = x.MerchantProductNo,
                    StockLocation = x.StockLocation
                }).ToList());
            }
            return orderLineswitStock;
        }
        /// <summary>
        /// Method to Update the stock of top 1 product
        /// </summary>
        /// <param name="stocks"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStock(List<Stock> stocks)
        {
            bool stockUpdateResult = false;
            var endPoint = "api/v2/offer/stock";
            var URL = baseUri + endPoint + "?apikey=" + apiKey;
            var _httpRequest = new HttpRequestMessage();
            var httpClient = new HttpClient();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("PUT");
            _httpRequest.RequestUri = new System.Uri(URL);
            string requestContent = SafeJsonConvert.SerializeObject(stocks, new JsonSerializerSettings());
            _httpRequest.Content = new StringContent(requestContent, System.Text.Encoding.UTF8);
            _httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            _httpResponse = await httpClient.SendAsync(_httpRequest);
            if (Convert.ToInt32(_httpResponse.StatusCode) == 200)
            {
                stockUpdateResult = true;
            }
            return stockUpdateResult;
        }
    }
}
