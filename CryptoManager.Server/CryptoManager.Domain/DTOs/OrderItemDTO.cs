using System;

namespace CryptoManager.Domain.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Fee { get; set; }
        public Guid FeeAssetId { get; set; }
    }
}