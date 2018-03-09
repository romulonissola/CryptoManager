using CryptoManager.Domain.Contracts.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoManager.Domain.Entities
{
    public class OrderItem : IEntity
    {
        public Guid Id { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Fee { get; set; }

        [ForeignKey("FeeAsset")]
        public Guid FeeAssetId { get; set; }
        public virtual Asset FeeAsset { get; set; }

        public bool IsExcluded { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}
