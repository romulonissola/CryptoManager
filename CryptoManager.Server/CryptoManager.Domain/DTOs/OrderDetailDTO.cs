using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Domain.DTOs
{
    public class OrderDetailDTO
    {
        public Guid Id { get; set; }
        public string ExchangeName { get; set; }
        public string BaseAssetSymbol { get; set; }
        public string QuoteAssetSymbol { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal ValuePaidWithFees { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal ValueSoldWithFees { get; set; }
        public decimal Profit { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
    }
}
