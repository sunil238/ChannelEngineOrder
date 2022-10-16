using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineOrder.Models
{
    public class ChannelOrder
    {
        public List<Content> Content { get; set; }
    }
    public class Content
    {
        public Int32 Id { get; set; }
        public string ChannelName { get; set; }
        public Int32 ChannelId { get; set; }
        public string GlobalChannelName { get; set; }
        public Int32 GlobalChannelId { get; set; }
        public OrderSupport ChannelOrderSupport { get; set; }
        public string ChannelOrderNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public OrderStatusView Status { get; set; }
        public Boolean IsBusinessOrder { get; set; }
        public string AcknowledgedDate { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string MerchantComment { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public ShoppingAddress ShoppingAddress { get; set; }
        public decimal SubTotalInclVat { get; set; }
        public decimal SubTotalVat { get; set; }
        public decimal ShippingCostsVat { get; set; }
        public decimal TotalInclVat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal OriginalSubTotalInclVat { get; set; }
        public decimal OriginalSubTotalVat { get; set; }
        public decimal OriginalShippingCostsInclVat { get; set; }
        public decimal OriginalShippingCostsVat { get; set; }
        public decimal OriginalTotalInclVat { get; set; }
        public decimal OriginalTotalVat { get; set; }
        public List<Lines> Lines { get; set; }
    }
    public enum OrderSupport
    {
        NONE, 
        ORDERS, 
        SPLIT_ORDERS, 
        SPLIT_ORDER_LINES
    }
    public enum OrderStatusView
    {
        IN_PROGRESS, 
        SHIPPED, 
        IN_BACKORDER, 
        MANCO, 
        CANCELED, 
        IN_COMBI, 
        CLOSED, 
        NEW, 
        RETURNED, 
        REQUIRES_CORRECTION, 
        AWAITING_PAYMENT
    }
    public class Lines
    {
        public OrderStatusView Status { get; set; }
        public bool IsFulfillmentByMarketplace { get; set; }
        public string Gtin { get; set; }
        public string Description { get; set; }
        public StockLocation StockLocation { get; set; }
        public decimal UnitVat { get; set; }
        public decimal LineTotalInclVat { get; set; }
        public decimal LineVat { get; set; }
        public decimal OriginalUnitPriceInclVat { get; set; }
        public decimal OriginalUnitVat { get; set; }
        public decimal OriginalLineTotalInclVat { get; set; }
        public decimal OriginalLineVat { get; set; }
        public decimal OriginalFeeFixed { get; set; }
        public string BundleProductMerchantProductNo { get; set; }
        public string JurisCode { get; set; }
        public string JurisName { get; set; }
        public decimal VatRate { get; set; }
        public ExtraData ExtraDate { get; set; }
        public string ChannelProductNo { get; set; }
        public string MerchantProductNo { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 CancellationRequestedQuantity { get; set; }
        public decimal UnitPriceInclVat { get; set; }
        public decimal FeeFixed { get; set; }
        public decimal FeeRate { get; set; }
        public Condition Condition { get; set; }
        public decimal ShippingCostsInclVat { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyRegistrationNo { get; set; }
        public string VatNo { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string CurrencyCode { get; set; }
        public string OrderDate { get; set; }
        public string ChannelCustomerNo { get; set; }
        public ExtraDataHeader ExtraDataHeader { get; set; }

    }
    public class ExtraDataHeader
    {
        public string description { get; set; }
    }
    public enum Condition
    {
        NEW, 
        NEW_REFURBISHED, 
        USED_AS_NEW, 
        USED_GOOD, 
        USED_REASONABLE, 
        USED_MEDIOCRE, 
        UNKNOWN, 
        USED_VERY_GOOD
    }
    public class ExtraData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class StockLocation
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
    }
    public class BillingAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public Gender Gender { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string HouseNr { get; set; }
        public string HouseNrAddition { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryIso { get; set; }
        public string Original { get; set; }

    }
    public class ShoppingAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public Gender Gender { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string HouseNr { get; set; }
        public string HouseNrAddition { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryIso { get; set; }
        public string Original { get; set; }

    }
    public enum Gender
    {
        MALE, 
        FEMALE, 
        NOT_APPLICABLE
    }
    
}
