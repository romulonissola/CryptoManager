using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoManager.Domain.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("BaseAsset")]
        public Guid BaseAssetId { get; set; }
        public virtual Asset BaseAsset { get; set; }

        [ForeignKey("QuoteAsset")]
        public Guid QuoteAssetId { get; set; }
        public virtual Asset QuoteAsset { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Exchange")]
        public Guid ExchangeId { get; set; }
        public virtual Exchange Exchange { get; set; }

        public bool IsViaRoboTrader { get; set; }
        public OrderType OrderType { get; set; }

        [ForeignKey("RelatedOrder")]
        public Guid? RelatedOrderId { get; set; }
        public virtual Order RelatedOrder { get; set; }

        public bool IsExcluded { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Order> RelatedOrders { get; set; }
    }
}
