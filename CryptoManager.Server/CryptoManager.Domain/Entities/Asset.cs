using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoManager.Domain.Entities
{
    public class Asset : IEntity
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }

        [InverseProperty("BaseAsset")]
        public virtual ICollection<Order> BaseOrders { get; set; }
        [InverseProperty("QuoteAsset")]
        public virtual ICollection<Order> QuoteOrders { get; set; }
        public virtual ICollection<OrderItem> OrderItems{ get; set; }
    }
}
