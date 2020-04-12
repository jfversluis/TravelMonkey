using System;

namespace TravelMonkey.Models.CognitiveServices
{
    public partial class AnalyzeReceiptResult
    {
        public string Status { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset LastUpdatedDateTime { get; set; }
        public AnalyzeResult AnalyzeResult { get; set; }
    }

    public partial class AnalyzeResult
    {
        public string Version { get; set; }
        public ReadResult[] ReadResults { get; set; }
        public DocumentResult[] DocumentResults { get; set; }
    }

    public partial class DocumentResult
    {
        public string DocType { get; set; }
        public long[] PageRange { get; set; }
        public Fields Fields { get; set; }
    }

    public partial class Fields
    {
        public ReceiptType ReceiptType { get; set; }
        public Name MerchantName { get; set; }
        public MerchantAddress MerchantAddress { get; set; }
        public MerchantPhoneNumber MerchantPhoneNumber { get; set; }
        public Items Items { get; set; }
    }

    public partial class Items
    {
        public string Type { get; set; }
        public ValueArray[] ValueArray { get; set; }
    }

    public partial class ValueArray
    {
        public string Type { get; set; }
        public ValueObject ValueObject { get; set; }
    }

    public partial class ValueObject
    {
        public Quantity Quantity { get; set; }
        public Name Name { get; set; }
        public TotalPrice TotalPrice { get; set; }
    }

    public partial class Name
    {
        public string Type { get; set; }
        public string ValueString { get; set; }
        public string Text { get; set; }
        public double[] BoundingBox { get; set; }
        public long Page { get; set; }
        public double Confidence { get; set; }
    }

    public partial class Quantity
    {
        public string Type { get; set; }
        public long Text { get; set; }
        public double[] BoundingBox { get; set; }
        public long Page { get; set; }
        public double Confidence { get; set; }
    }

    public partial class TotalPrice
    {
        public string Type { get; set; }
        public double? ValueNumber { get; set; }
        public string Text { get; set; }
        public double[] BoundingBox { get; set; }
        public long Page { get; set; }
        public double Confidence { get; set; }
    }

    public partial class MerchantAddress
    {
        public string Type { get; set; }
        public string ValueString { get; set; }
        public string Text { get; set; }
        public long[] BoundingBox { get; set; }
        public long Page { get; set; }
        public double Confidence { get; set; }
    }

    public partial class MerchantPhoneNumber
    {
        public string Type { get; set; }
        public string ValuePhoneNumber { get; set; }
        public string Text { get; set; }
        public long[] BoundingBox { get; set; }
        public long Page { get; set; }
        public double Confidence { get; set; }
    }

    public partial class ReceiptType
    {
        public string Type { get; set; }
        public string ValueString { get; set; }
        public double Confidence { get; set; }
    }

    public partial class ReadResult
    {
        public long Page { get; set; }
        public double Angle { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public string Unit { get; set; }
        public string Language { get; set; }
    }
}